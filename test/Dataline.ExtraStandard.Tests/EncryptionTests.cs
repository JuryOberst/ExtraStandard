using Org.BouncyCastle.Pkcs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;

using Xunit;

namespace ExtraStandard.Tests
{
    public class EncryptionTests
    {
        private readonly X509Certificate2 _tempCert;

        public EncryptionTests()
        {
            _tempCert = Utils.CreateTemporaryCertificate("Name", "name@firma.com", "ProdId", "ProdVersion", "Ort", "Staat");
        }

#if NET47
        [Fact]
        public void EncryptionNativeTest()
        {
            const string TestString = "Hello World!";
            var senderCert = new X509Certificate2(_tempCert.Export(X509ContentType.Pkcs12));
            var receiverCert = new X509Certificate2(_tempCert.Export(X509ContentType.Cert));
            var encryption = new ExtraStandard.Encryption.NativePkcs7EncryptionHandler(senderCert, receiverCert);
            var testData = Encoding.UTF8.GetBytes(TestString);
            var timestamp = DateTime.Now;
            var encrypted = encryption.Encrypt(testData, timestamp);
            var decrypted = encryption.Decrypt(encrypted);
            Assert.Equal(TestString, Encoding.UTF8.GetString(decrypted));
        }

        [Fact]
        public void EncryptionNativeWithBouncyCastleCertTest()
        {
            const string TestString = "Hello World!";
            var senderCert = new Pkcs12Store(new MemoryStream(_tempCert.Export(X509ContentType.Pkcs12)), new char[0]);
            var receiverCert = Utils.FromX509Certificate(_tempCert);
            var encryption = new ExtraStandard.Encryption.NativePkcs7EncryptionHandler(senderCert, receiverCert);
            var testData = Encoding.UTF8.GetBytes(TestString);
            var timestamp = DateTime.Now;
            var encrypted = encryption.Encrypt(testData, timestamp);
            var decrypted = encryption.Decrypt(encrypted);
            Assert.Equal(TestString, Encoding.UTF8.GetString(decrypted));
        }
#endif

        [Fact]
        public void EncryptionBouncyCastleTest()
        {
            const string TestString = "Hello World!";
            var senderCert = new Pkcs12Store(new MemoryStream(_tempCert.Export(X509ContentType.Pkcs12)), new char[0]);
            var receiverCert = Utils.FromX509Certificate(_tempCert);
            var encryption = new ExtraStandard.Encryption.BouncyCastlePkcs7EncryptionHandler(senderCert, receiverCert);
            var testData = Encoding.UTF8.GetBytes(TestString);
            var timestamp = DateTime.Now;
            var encrypted = encryption.Encrypt(testData, timestamp);
            var decrypted = encryption.Decrypt(encrypted);
            Assert.Equal(TestString, Encoding.UTF8.GetString(decrypted));
        }

        [Fact]
        public void EncryptionBouncyCastleWithNativeCertTest()
        {
            const string TestString = "Hello World!";
            var senderCert = new X509Certificate2(_tempCert.Export(X509ContentType.Pkcs12), (string)null, X509KeyStorageFlags.Exportable);
            var receiverCert = new X509Certificate2(_tempCert.Export(X509ContentType.Cert), (string)null, X509KeyStorageFlags.Exportable);
            var encryption = new ExtraStandard.Encryption.BouncyCastlePkcs7EncryptionHandler(senderCert, receiverCert);
            var testData = Encoding.UTF8.GetBytes(TestString);
            var timestamp = DateTime.Now;
            var encrypted = encryption.Encrypt(testData, timestamp);
            var decrypted = encryption.Decrypt(encrypted);
            Assert.Equal(TestString, Encoding.UTF8.GetString(decrypted));
        }
    }
}
