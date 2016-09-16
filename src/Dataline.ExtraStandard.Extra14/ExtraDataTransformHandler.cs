using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace ExtraStandard.Extra14
{
    /// <summary>
    /// Führt Daten-Transformationen aus
    /// </summary>
    public class ExtraDataTransformHandler
    {
        private readonly IDictionary<string, IExtraCompressionHandler> _compressionHandlers;
        private readonly IDictionary<string, IExtraEncryptionHandler> _encryptionHandlers;

        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="ExtraDataTransformHandler"/> Klasse.
        /// </summary>
        /// <param name="compressionHandlers">Die erlaubten Kompressionsverfahren</param>
        /// <param name="encryptionHandlers">Die erlaubten Krypto-Verfahren</param>
        public ExtraDataTransformHandler(
            IEnumerable<IExtraCompressionHandler> compressionHandlers,
            IEnumerable<IExtraEncryptionHandler> encryptionHandlers)
        {
            _compressionHandlers = compressionHandlers.ToDictionary(x => x.AlgorithmId);
            _encryptionHandlers = encryptionHandlers.ToDictionary(x => x.AlgorithmId);
        }

        /// <summary>
        /// Führt die angegebenen <paramref name="transformAlgorithmIds"/> auf den übergebenen Daten aus.
        /// </summary>
        /// <param name="data">Die übergebenen Daten</param>
        /// <param name="dataName">Der Name der übergebenen Daten (nur relevant bei ZIP-Kompression)</param>
        /// <param name="requestTimestamp">Der Zeitstempel der Daten-Übermittlung (nur relevant bei PKCS#7-Verschlüsselung)</param>
        /// <param name="transformAlgorithmIds">Die zu verwendenden Daten-Transformationen</param>
        /// <returns>Die transformierten Daten</returns>
        public Tuple<byte[], IEnumerable<AbstractTransformType>> Transform(byte[] data, string dataName, DateTime requestTimestamp, params string[] transformAlgorithmIds)
        {
            var transforms = new List<AbstractTransformType>();
            var output = data;
            var index = 1;
            foreach (var transformAlgorithmId in transformAlgorithmIds)
            {
                IExtraCompressionHandler compressionHandler;
                if (_compressionHandlers.TryGetValue(transformAlgorithmId, out compressionHandler))
                {
                    var transform = new CompressionType()
                    {
                        Algorithm = new CompressionAlgorithmType() {id = compressionHandler.AlgorithmId},
                        order = XmlConvert.ToString(index),
                    };

                    if (AddDataInfo(index == 1, index == transformAlgorithmIds.Length, index, true))
                    {
                        transform.InputData = new DataType()
                        {
                            bytes = XmlConvert.ToString(output.Length)
                        };
                    }

                    output = compressionHandler.Compress(output, dataName);

                    if (AddDataInfo(index == 1, index == transformAlgorithmIds.Length, index, false))
                    {
                        transform.OutputData = new DataType()
                        {
                            bytes = XmlConvert.ToString(output.Length)
                        };
                    }

                    transforms.Add(transform);
                }
                else
                {
                    IExtraEncryptionHandler encryptionHandler;
                    if (_encryptionHandlers.TryGetValue(transformAlgorithmId, out encryptionHandler))
                    {
                        var transform = new EncryptionType()
                        {
                            Algorithm = new EncryptionAlgorithmType() { id = encryptionHandler.AlgorithmId },
                            order = XmlConvert.ToString(index),
                        };

                        if (AddDataInfo(index == 1, index == transformAlgorithmIds.Length, index, true))
                        {
                            transform.InputData = new DataType()
                            {
                                bytes = XmlConvert.ToString(output.Length)
                            };
                        }

                        output = encryptionHandler.Encrypt(output, requestTimestamp);

                        if (AddDataInfo(index == 1, index == transformAlgorithmIds.Length, index, false))
                        {
                            transform.OutputData = new DataType()
                            {
                                bytes = XmlConvert.ToString(output.Length)
                            };
                        }

                        transforms.Add(transform);
                    }
                    else
                    {
                        throw new NotSupportedException($"Eine Transformation mit der ID {transformAlgorithmId} wird nicht unterstützt.");
                    }
                }

                index += 1;
            }

            return Tuple.Create<byte[], IEnumerable<AbstractTransformType>>(output, transforms);
        }

        /// <summary>
        /// Daten-Transformationen rückgängig machen (für die Verarbeitung der eXTra-Antworten)
        /// </summary>
        /// <param name="data">Die vom Absender transformierten Daten</param>
        /// <param name="dataTransforms">Die vom Absender durchgeführten Transformationen</param>
        /// <returns>Die ursprünglichen (nicht mehr transformierten) Daten</returns>
        public byte[] ReverseTransform(byte[] data, DataTransformsType dataTransforms)
        {
            var output = data;
            var transforms = dataTransforms
                .Encryption.Cast<AbstractTransformType>().Concat(dataTransforms.Compression)
                .OrderByDescending(x => XmlConvert.ToInt32(x.order));
            foreach (var transform in transforms)
            {
                var encryption = transform as EncryptionType;
                if (encryption != null)
                {
                    var handler = _encryptionHandlers[encryption.Algorithm.id];
                    ValidatePayloadSize(output, encryption.OutputData);
                    output = handler.Decrypt(output);
                    ValidatePayloadSize(output, encryption.InputData);
                }
                else
                {
                    var compression = transform as CompressionType;
                    if (compression != null)
                    {
                        var handler = _compressionHandlers[compression.Algorithm.id];
                        ValidatePayloadSize(output, compression.OutputData);
                        output = handler.Decompress(output);
                        ValidatePayloadSize(output, compression.InputData);
                    }
                    else
                    {
                        throw new NotSupportedException($"Eine Transformation des Types {transform.GetType().Name} wird nicht unterstützt.");
                    }
                }
            }

            return output;
        }

        /// <summary>
        /// Liefert <code>true</code>, wenn ein <see cref="DataType"/>-Element während eines <see cref="Transform"/> erstellt werden soll.
        /// </summary>
        /// <param name="first">Ist es die erste Transformation?</param>
        /// <param name="last">Ist es die letzte Transformation?</param>
        /// <param name="index">Nummer der Transformation (beginnend bei 1)</param>
        /// <param name="forInput">Ist für die Daten-Transformation die Eingangsgröße oder die Ausgangsgröße gemeint?</param>
        /// <returns><code>true</code>, wenn ein <see cref="DataType"/> erstellt werden soll</returns>
        /// <remarks>
        /// Standardmäßig werden nur die <see cref="DataType"/>-Elemente für die Eingabe-Daten der ersten Transformation
        /// und für die Ausgabe-Daten der letzten Transformation erstellt.
        /// </remarks>
        public virtual bool AddDataInfo(bool first, bool last, int index, bool forInput)
        {
            return first && forInput || last && !forInput;
        }

        private void ValidatePayloadSize(byte[] payload, DataType dataInfo)
        {
            if (dataInfo == null)
                return;
            var count = XmlConvert.ToInt32(dataInfo.bytes);
            if (count != payload.Length)
                throw new ExtraException($"Die angegebene Datengröße ({count}) weicht von der tatsächlichen Datengröße ({payload.Length}) ab.");
        }
    }
}
