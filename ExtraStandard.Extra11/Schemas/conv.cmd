@echo off

REM xsd /classes /n:ExtraStandard.Extra11 /edb /order /o:. xmldsig-core-schema.xsd xenc-schema.xsd eXTra-codelists-1.xsd eXTra-components-1.xsd eXTra-logging-1.xsd eXTra-messages-1.xsd eXTra-plugins-1.xsd .\eXTra-request-1.xsd
REM xsd /classes /n:ExtraStandard.Extra11 /edb /order /o:. xmldsig-core-schema.xsd xenc-schema.xsd eXTra-codelists-1.xsd eXTra-components-1.xsd eXTra-logging-1.xsd eXTra-messages-1.xsd eXTra-plugins-1.xsd .\eXTra-response-1.xsd

xsd /classes /n:ExtraStandard.Extra11 /edb /order /o:. xmldsig-core-schema.xsd xenc-schema.xsd eXTra-codelists-1.xsd eXTra-components-1.xsd eXTra-logging-1.xsd eXTra-messages-1.xsd eXTra-plugins-1.xsd eXTra-response-1.xsd .\eXTra-request-1.xsd
