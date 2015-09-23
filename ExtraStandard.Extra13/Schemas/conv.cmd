@echo off

REM xmldsig
xsd /classes /n:ExtraStandard.Extra13 /edb /order /o:. .\xmldsig-core-schema.xsd 

REM XML Encoding
xsd /classes /n:ExtraStandard.Extra13 /edb /order /o:. xmldsig-core-schema.xsd .\xenc-schema.xsd 

REM Codelists
REM xsd /classes /n:ExtraStandard.Extra13 /edb /order /o:. KomServer-0-codelists-1.xsd
REM xsd /classes /n:ExtraStandard.Extra13 /edb /order /o:. KomServer-1-codelists-1.xsd
REM xsd /classes /n:ExtraStandard.Extra13 /edb /order /o:. KomServer-2-codelists-1.xsd
REM xsd /classes /n:ExtraStandard.Extra13 /edb /order /o:. KomServer-3-codelists-1.xsd

REM Components, Plugins
xsd /classes /n:ExtraStandard.Extra13 /edb /order /o:. xmldsig-core-schema.xsd KomServer-plugins-1-common.xsd KomServer-0-plugins-1.xsd KomServer-0-codelists-1.xsd KomServer-components-1-common.xsd .\KomServer-0-components-1.xsd 
xsd /classes /n:ExtraStandard.Extra13 /edb /order /o:. xmldsig-core-schema.xsd KomServer-plugins-1-common.xsd KomServer-1-plugins-1.xsd KomServer-1-codelists-1.xsd KomServer-components-1-common.xsd .\KomServer-1-components-1.xsd 
xsd /classes /n:ExtraStandard.Extra13 /edb /order /o:. xmldsig-core-schema.xsd KomServer-plugins-1-common.xsd KomServer-2-plugins-1.xsd KomServer-2-codelists-1.xsd KomServer-components-1-common.xsd .\KomServer-2-components-1.xsd 
xsd /classes /n:ExtraStandard.Extra13 /edb /order /o:. xmldsig-core-schema.xsd KomServer-plugins-1-common.xsd KomServer-3-plugins-1.xsd KomServer-3-codelists-1.xsd KomServer-components-1-common.xsd .\KomServer-3-components-1.xsd 

REM Logging
xsd /classes /n:ExtraStandard.Extra13 /edb /order /o:. xmldsig-core-schema.xsd xenc-schema.xsd KomServer-1-plugins-1.xsd KomServer-1-codelists-1.xsd KomServer-1-components-1.xsd .\KomServer-1-logging-1.xsd 
xsd /classes /n:ExtraStandard.Extra13 /edb /order /o:. xmldsig-core-schema.xsd xenc-schema.xsd KomServer-2-plugins-1.xsd KomServer-2-codelists-1.xsd KomServer-2-components-1.xsd .\KomServer-2-logging-1.xsd 
xsd /classes /n:ExtraStandard.Extra13 /edb /order /o:. xmldsig-core-schema.xsd xenc-schema.xsd KomServer-3-plugins-1.xsd KomServer-3-codelists-1.xsd KomServer-3-components-1.xsd .\KomServer-3-logging-1.xsd 

REM Error
xsd /classes /n:ExtraStandard.Extra13 /edb /order /o:. xmldsig-core-schema.xsd KomServer-plugins-1-common.xsd KomServer-0-plugins-1.xsd KomServer-0-codelists-1.xsd KomServer-components-1-common.xsd KomServer-0-components-1.xsd KomServer-0-error-1.xsd .\xsd_KomServer_0_error.xsd

REM ExTRA Deliver (Request / Response)
xsd /classes /n:ExtraStandard.Extra13 /edb /order /o:. xmldsig-core-schema.xsd xenc-schema.xsd KomServer-plugins-1-common.xsd KomServer-1-plugins-1.xsd KomServer-1-codelists-1.xsd KomServer-components-1-common.xsd KomServer-1-components-1.xsd KomServer-1-logging-1.xsd KomServer-1-request-1.xsd .\xsd_KomServer_1_request_senden_datenlieferungen.xsd
xsd /classes /n:ExtraStandard.Extra13 /edb /order /o:. xmldsig-core-schema.xsd xenc-schema.xsd KomServer-plugins-1-common.xsd KomServer-1-plugins-1.xsd KomServer-1-codelists-1.xsd KomServer-components-1-common.xsd KomServer-1-components-1.xsd KomServer-1-logging-1.xsd KomServer-1-response-1.xsd .\xsd_KomServer_2_response_senden_datenlieferungen.xsd

REM ExTRA Query (Request body / Request / Response)
xsd /classes /n:ExtraStandard.Extra13 /edb /order /o:. KomServer-2-DataRequest-1.xsd .\xsd_KomServer_3_Body_request_anfordern_rueckmeldungen.xsd
xsd /classes /n:ExtraStandard.Extra13 /edb /order /o:. xmldsig-core-schema.xsd xenc-schema.xsd KomServer-plugins-1-common.xsd KomServer-2-plugins-1.xsd KomServer-2-codelists-1.xsd KomServer-components-1-common.xsd KomServer-2-components-1.xsd KomServer-2-logging-1.xsd KomServer-2-request-1.xsd .\xsd_KomServer_3_request_anfordern_rueckmeldungen.xsd
xsd /classes /n:ExtraStandard.Extra13 /edb /order /o:. xmldsig-core-schema.xsd xenc-schema.xsd KomServer-plugins-1-common.xsd KomServer-2-plugins-1.xsd KomServer-2-codelists-1.xsd KomServer-components-1-common.xsd KomServer-2-components-1.xsd KomServer-2-logging-1.xsd KomServer-2-response-1.xsd .\xsd_KomServer_4_response_abholen_rueckmeldungen.xsd

REM ExTRA Acknowledge (Request body / Request / Response)
xsd /classes /n:ExtraStandard.Extra13 /edb /order /o:. KomServer-Empfangsquittung-1.xsd .\xsd_KomServer_5_Body_request_senden_empfangsquittungen.xsd
xsd /classes /n:ExtraStandard.Extra13 /edb /order /o:. xmldsig-core-schema.xsd xenc-schema.xsd KomServer-plugins-1-common.xsd KomServer-3-plugins-1.xsd KomServer-3-codelists-1.xsd KomServer-components-1-common.xsd KomServer-3-components-1.xsd KomServer-3-logging-1.xsd KomServer-3-request-1.xsd .\xsd_KomServer_5_request_senden_empfangsquittungen.xsd
xsd /classes /n:ExtraStandard.Extra13 /edb /order /o:. xmldsig-core-schema.xsd xenc-schema.xsd KomServer-plugins-1-common.xsd KomServer-3-plugins-1.xsd KomServer-3-codelists-1.xsd KomServer-components-1-common.xsd KomServer-3-components-1.xsd KomServer-3-logging-1.xsd KomServer-3-response-1.xsd .\xsd_KomServer_6_response_senden_empfangsquittungen.xsd
