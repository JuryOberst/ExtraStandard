@echo off

REM xmldsig und XML Encoding
REM xsd /classes /n:ExtraStandard.Extra14 /edb /order /o:. xmldsig-core-schema.xsd .\xenc-schema.xsd

REM Codelists
REM xsd /classes /n:ExtraStandard.Extra14 /edb /order /o:. .\eXTra-codelists-1.xsd

REM Components, Plugins
REM xsd /classes /n:ExtraStandard.Extra14 /edb /order /o:. xmldsig-core-schema.xsd xenc-schema.xsd eXTra-codelists-1.xsd eXTra-plugins-1.xsd .\eXTra-components-1.xsd

REM Logging
REM xsd /classes /n:ExtraStandard.Extra14 /edb /order /o:. xmldsig-core-schema.xsd xenc-schema.xsd eXTra-codelists-1.xsd eXTra-plugins-1.xsd eXTra-components-1.xsd .\eXTra-logging-1.xsd

REM Messages
REM xsd /classes /n:ExtraStandard.Extra14 /edb /order /o:. xmldsig-core-schema.xsd xenc-schema.xsd eXTra-codelists-1.xsd eXTra-plugins-1.xsd eXTra-components-1.xsd .\extra-messages-1.xsd

REM Error
REM xsd /classes /n:ExtraStandard.Extra14 /edb /order /o:. xmldsig-core-schema.xsd xenc-schema.xsd eXTra-codelists-1.xsd eXTra-plugins-1.xsd eXTra-components-1.xsd .\extra-error-1.xsd

REM ExTRA Request / Response
REM xsd /classes /n:ExtraStandard.Extra14 /edb /order /o:. xmldsig-core-schema.xsd xenc-schema.xsd eXTra-codelists-1.xsd eXTra-plugins-1.xsd eXTra-components-1.xsd eXTra-logging-1.xsd .\eXTra-request-1.xsd
REM xsd /classes /n:ExtraStandard.Extra14 /edb /order /o:. xmldsig-core-schema.xsd xenc-schema.xsd eXTra-codelists-1.xsd eXTra-plugins-1.xsd eXTra-components-1.xsd eXTra-logging-1.xsd .\eXTra-response-1.xsd

xsd /classes /n:ExtraStandard.Extra14 /edb /order /o:. xmldsig-core-schema.xsd xenc-schema.xsd eXTra-codelists-1.xsd eXTra-plugins-1.xsd eXTra-components-1.xsd eXTra-logging-1.xsd eXTra-messages-1.xsd extra-error-1.xsd eXTra-response-1.xsd .\eXTra-request-1.xsd
