using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SkyDCore.Net
{
    /// <summary>
    /// 文件类型枚举，可以通过GetDescription扩展方法获取其对应的MIME类型。
    /// </summary>
    public enum MIME
    {

        /// <summary>
        /// 扩展名.323的文件，对应ContentType(MIME)类型：text/h323
        /// </summary>
        [Description("text/h323")]
        _323,
        /// <summary>
        /// 扩展名.3gp的文件，对应ContentType(MIME)类型：video/3gpp
        /// </summary>
        [Description("video/3gpp")]
        _3gp,
        /// <summary>
        /// 扩展名.aab的文件，对应ContentType(MIME)类型：application/x-authoware-bin
        /// </summary>
        [Description("application/x-authoware-bin")]
        _aab,
        /// <summary>
        /// 扩展名.aam的文件，对应ContentType(MIME)类型：application/x-authoware-map
        /// </summary>
        [Description("application/x-authoware-map")]
        _aam,
        /// <summary>
        /// 扩展名.aas的文件，对应ContentType(MIME)类型：application/x-authoware-seg
        /// </summary>
        [Description("application/x-authoware-seg")]
        _aas,
        /// <summary>
        /// 扩展名.acx的文件，对应ContentType(MIME)类型：application/internet-property-stream
        /// </summary>
        [Description("application/internet-property-stream")]
        _acx,
        /// <summary>
        /// 扩展名.ai的文件，对应ContentType(MIME)类型：application/postscript
        /// </summary>
        [Description("application/postscript")]
        _ai,
        /// <summary>
        /// 扩展名.aif的文件，对应ContentType(MIME)类型：audio/x-aiff
        /// </summary>
        [Description("audio/x-aiff")]
        _aif,
        /// <summary>
        /// 扩展名.aifc的文件，对应ContentType(MIME)类型：audio/x-aiff
        /// </summary>
        [Description("audio/x-aiff")]
        _aifc,
        /// <summary>
        /// 扩展名.aiff的文件，对应ContentType(MIME)类型：audio/x-aiff
        /// </summary>
        [Description("audio/x-aiff")]
        _aiff,
        /// <summary>
        /// 扩展名.als的文件，对应ContentType(MIME)类型：audio/X-Alpha5
        /// </summary>
        [Description("audio/X-Alpha5")]
        _als,
        /// <summary>
        /// 扩展名.amc的文件，对应ContentType(MIME)类型：application/x-mpeg
        /// </summary>
        [Description("application/x-mpeg")]
        _amc,
        /// <summary>
        /// 扩展名.ani的文件，对应ContentType(MIME)类型：application/octet-stream
        /// </summary>
        [Description("application/octet-stream")]
        _ani,
        /// <summary>
        /// 扩展名.apk的文件，对应ContentType(MIME)类型：application/vnd.android.package-archive
        /// </summary>
        [Description("application/vnd.android.package-archive")]
        _apk,
        /// <summary>
        /// 扩展名.asc的文件，对应ContentType(MIME)类型：text/plain
        /// </summary>
        [Description("text/plain")]
        _asc,
        /// <summary>
        /// 扩展名.asd的文件，对应ContentType(MIME)类型：application/astound
        /// </summary>
        [Description("application/astound")]
        _asd,
        /// <summary>
        /// 扩展名.asf的文件，对应ContentType(MIME)类型：video/x-ms-asf
        /// </summary>
        [Description("video/x-ms-asf")]
        _asf,
        /// <summary>
        /// 扩展名.asn的文件，对应ContentType(MIME)类型：application/astound
        /// </summary>
        [Description("application/astound")]
        _asn,
        /// <summary>
        /// 扩展名.asp的文件，对应ContentType(MIME)类型：application/x-asap
        /// </summary>
        [Description("application/x-asap")]
        _asp,
        /// <summary>
        /// 扩展名.asr的文件，对应ContentType(MIME)类型：video/x-ms-asf
        /// </summary>
        [Description("video/x-ms-asf")]
        _asr,
        /// <summary>
        /// 扩展名.asx的文件，对应ContentType(MIME)类型：video/x-ms-asf
        /// </summary>
        [Description("video/x-ms-asf")]
        _asx,
        /// <summary>
        /// 扩展名.au的文件，对应ContentType(MIME)类型：audio/basic
        /// </summary>
        [Description("audio/basic")]
        _au,
        /// <summary>
        /// 扩展名.avb的文件，对应ContentType(MIME)类型：application/octet-stream
        /// </summary>
        [Description("application/octet-stream")]
        _avb,
        /// <summary>
        /// 扩展名.avi的文件，对应ContentType(MIME)类型：video/x-msvideo
        /// </summary>
        [Description("video/x-msvideo")]
        _avi,
        /// <summary>
        /// 扩展名.awb的文件，对应ContentType(MIME)类型：audio/amr-wb
        /// </summary>
        [Description("audio/amr-wb")]
        _awb,
        /// <summary>
        /// 扩展名.axs的文件，对应ContentType(MIME)类型：application/olescript
        /// </summary>
        [Description("application/olescript")]
        _axs,
        /// <summary>
        /// 扩展名.bas的文件，对应ContentType(MIME)类型：text/plain
        /// </summary>
        [Description("text/plain")]
        _bas,
        /// <summary>
        /// 扩展名.bcpio的文件，对应ContentType(MIME)类型：application/x-bcpio
        /// </summary>
        [Description("application/x-bcpio")]
        _bcpio,
        /// <summary>
        /// 扩展名.bld的文件，对应ContentType(MIME)类型：application/bld
        /// </summary>
        [Description("application/bld")]
        _bld,
        /// <summary>
        /// 扩展名.bld2的文件，对应ContentType(MIME)类型：application/bld2
        /// </summary>
        [Description("application/bld2")]
        _bld2,
        /// <summary>
        /// 扩展名.bmp的文件，对应ContentType(MIME)类型：image/bmp
        /// </summary>
        [Description("image/bmp")]
        _bmp,
        /// <summary>
        /// 扩展名.bpk的文件，对应ContentType(MIME)类型：application/octet-stream
        /// </summary>
        [Description("application/octet-stream")]
        _bpk,
        /// <summary>
        /// 扩展名.bz2的文件，对应ContentType(MIME)类型：application/x-bzip2
        /// </summary>
        [Description("application/x-bzip2")]
        _bz2,
        /// <summary>
        /// 扩展名.c的文件，对应ContentType(MIME)类型：text/plain
        /// </summary>
        [Description("text/plain")]
        _c,
        /// <summary>
        /// 扩展名.cal的文件，对应ContentType(MIME)类型：image/x-cals
        /// </summary>
        [Description("image/x-cals")]
        _cal,
        /// <summary>
        /// 扩展名.cat的文件，对应ContentType(MIME)类型：application/vnd.ms-pkiseccat
        /// </summary>
        [Description("application/vnd.ms-pkiseccat")]
        _cat,
        /// <summary>
        /// 扩展名.ccn的文件，对应ContentType(MIME)类型：application/x-cnc
        /// </summary>
        [Description("application/x-cnc")]
        _ccn,
        /// <summary>
        /// 扩展名.cco的文件，对应ContentType(MIME)类型：application/x-cocoa
        /// </summary>
        [Description("application/x-cocoa")]
        _cco,
        /// <summary>
        /// 扩展名.cdf的文件，对应ContentType(MIME)类型：application/x-cdf
        /// </summary>
        [Description("application/x-cdf")]
        _cdf,
        /// <summary>
        /// 扩展名.cer的文件，对应ContentType(MIME)类型：application/x-x509-ca-cert
        /// </summary>
        [Description("application/x-x509-ca-cert")]
        _cer,
        /// <summary>
        /// 扩展名.cgi的文件，对应ContentType(MIME)类型：magnus-internal/cgi
        /// </summary>
        [Description("magnus-internal/cgi")]
        _cgi,
        /// <summary>
        /// 扩展名.chat的文件，对应ContentType(MIME)类型：application/x-chat
        /// </summary>
        [Description("application/x-chat")]
        _chat,
        /// <summary>
        /// 扩展名.class的文件，对应ContentType(MIME)类型：application/octet-stream
        /// </summary>
        [Description("application/octet-stream")]
        _class,
        /// <summary>
        /// 扩展名.clp的文件，对应ContentType(MIME)类型：application/x-msclip
        /// </summary>
        [Description("application/x-msclip")]
        _clp,
        /// <summary>
        /// 扩展名.cmx的文件，对应ContentType(MIME)类型：image/x-cmx
        /// </summary>
        [Description("image/x-cmx")]
        _cmx,
        /// <summary>
        /// 扩展名.co的文件，对应ContentType(MIME)类型：application/x-cult3d-object
        /// </summary>
        [Description("application/x-cult3d-object")]
        _co,
        /// <summary>
        /// 扩展名.cod的文件，对应ContentType(MIME)类型：image/cis-cod
        /// </summary>
        [Description("image/cis-cod")]
        _cod,
        /// <summary>
        /// 扩展名.conf的文件，对应ContentType(MIME)类型：text/plain
        /// </summary>
        [Description("text/plain")]
        _conf,
        /// <summary>
        /// 扩展名.cpio的文件，对应ContentType(MIME)类型：application/x-cpio
        /// </summary>
        [Description("application/x-cpio")]
        _cpio,
        /// <summary>
        /// 扩展名.cpp的文件，对应ContentType(MIME)类型：text/plain
        /// </summary>
        [Description("text/plain")]
        _cpp,
        /// <summary>
        /// 扩展名.cpt的文件，对应ContentType(MIME)类型：application/mac-compactpro
        /// </summary>
        [Description("application/mac-compactpro")]
        _cpt,
        /// <summary>
        /// 扩展名.crd的文件，对应ContentType(MIME)类型：application/x-mscardfile
        /// </summary>
        [Description("application/x-mscardfile")]
        _crd,
        /// <summary>
        /// 扩展名.crl的文件，对应ContentType(MIME)类型：application/pkix-crl
        /// </summary>
        [Description("application/pkix-crl")]
        _crl,
        /// <summary>
        /// 扩展名.crt的文件，对应ContentType(MIME)类型：application/x-x509-ca-cert
        /// </summary>
        [Description("application/x-x509-ca-cert")]
        _crt,
        /// <summary>
        /// 扩展名.csh的文件，对应ContentType(MIME)类型：application/x-csh
        /// </summary>
        [Description("application/x-csh")]
        _csh,
        /// <summary>
        /// 扩展名.csm的文件，对应ContentType(MIME)类型：chemical/x-csml
        /// </summary>
        [Description("chemical/x-csml")]
        _csm,
        /// <summary>
        /// 扩展名.csml的文件，对应ContentType(MIME)类型：chemical/x-csml
        /// </summary>
        [Description("chemical/x-csml")]
        _csml,
        /// <summary>
        /// 扩展名.css的文件，对应ContentType(MIME)类型：text/css
        /// </summary>
        [Description("text/css")]
        _css,
        /// <summary>
        /// 扩展名.cur的文件，对应ContentType(MIME)类型：application/octet-stream
        /// </summary>
        [Description("application/octet-stream")]
        _cur,
        /// <summary>
        /// 扩展名.dcm的文件，对应ContentType(MIME)类型：x-lml/x-evm
        /// </summary>
        [Description("x-lml/x-evm")]
        _dcm,
        /// <summary>
        /// 扩展名.dcr的文件，对应ContentType(MIME)类型：application/x-director
        /// </summary>
        [Description("application/x-director")]
        _dcr,
        /// <summary>
        /// 扩展名.dcx的文件，对应ContentType(MIME)类型：image/x-dcx
        /// </summary>
        [Description("image/x-dcx")]
        _dcx,
        /// <summary>
        /// 扩展名.der的文件，对应ContentType(MIME)类型：application/x-x509-ca-cert
        /// </summary>
        [Description("application/x-x509-ca-cert")]
        _der,
        /// <summary>
        /// 扩展名.dhtml的文件，对应ContentType(MIME)类型：text/html
        /// </summary>
        [Description("text/html")]
        _dhtml,
        /// <summary>
        /// 扩展名.dir的文件，对应ContentType(MIME)类型：application/x-director
        /// </summary>
        [Description("application/x-director")]
        _dir,
        /// <summary>
        /// 扩展名.dll的文件，对应ContentType(MIME)类型：application/x-msdownload
        /// </summary>
        [Description("application/x-msdownload")]
        _dll,
        /// <summary>
        /// 扩展名.dmg的文件，对应ContentType(MIME)类型：application/octet-stream
        /// </summary>
        [Description("application/octet-stream")]
        _dmg,
        /// <summary>
        /// 扩展名.dms的文件，对应ContentType(MIME)类型：application/octet-stream
        /// </summary>
        [Description("application/octet-stream")]
        _dms,
        /// <summary>
        /// 扩展名.doc的文件，对应ContentType(MIME)类型：application/msword
        /// </summary>
        [Description("application/msword")]
        _doc,
        /// <summary>
        /// 扩展名.docx的文件，对应ContentType(MIME)类型：application/vnd.openxmlformats-officedocument.wordprocessingml.document
        /// </summary>
        [Description("application/vnd.openxmlformats-officedocument.wordprocessingml.document")]
        _docx,
        /// <summary>
        /// 扩展名.dot的文件，对应ContentType(MIME)类型：application/msword
        /// </summary>
        [Description("application/msword")]
        _dot,
        /// <summary>
        /// 扩展名.dvi的文件，对应ContentType(MIME)类型：application/x-dvi
        /// </summary>
        [Description("application/x-dvi")]
        _dvi,
        /// <summary>
        /// 扩展名.dwf的文件，对应ContentType(MIME)类型：drawing/x-dwf
        /// </summary>
        [Description("drawing/x-dwf")]
        _dwf,
        /// <summary>
        /// 扩展名.dwg的文件，对应ContentType(MIME)类型：application/x-autocad
        /// </summary>
        [Description("application/x-autocad")]
        _dwg,
        /// <summary>
        /// 扩展名.dxf的文件，对应ContentType(MIME)类型：application/x-autocad
        /// </summary>
        [Description("application/x-autocad")]
        _dxf,
        /// <summary>
        /// 扩展名.dxr的文件，对应ContentType(MIME)类型：application/x-director
        /// </summary>
        [Description("application/x-director")]
        _dxr,
        /// <summary>
        /// 扩展名.ebk的文件，对应ContentType(MIME)类型：application/x-expandedbook
        /// </summary>
        [Description("application/x-expandedbook")]
        _ebk,
        /// <summary>
        /// 扩展名.emb的文件，对应ContentType(MIME)类型：chemical/x-embl-dl-nucleotide
        /// </summary>
        [Description("chemical/x-embl-dl-nucleotide")]
        _emb,
        /// <summary>
        /// 扩展名.embl的文件，对应ContentType(MIME)类型：chemical/x-embl-dl-nucleotide
        /// </summary>
        [Description("chemical/x-embl-dl-nucleotide")]
        _embl,
        /// <summary>
        /// 扩展名.eps的文件，对应ContentType(MIME)类型：application/postscript
        /// </summary>
        [Description("application/postscript")]
        _eps,
        /// <summary>
        /// 扩展名.epub的文件，对应ContentType(MIME)类型：application/epub+zip
        /// </summary>
        [Description("application/epub+zip")]
        _epub,
        /// <summary>
        /// 扩展名.eri的文件，对应ContentType(MIME)类型：image/x-eri
        /// </summary>
        [Description("image/x-eri")]
        _eri,
        /// <summary>
        /// 扩展名.es的文件，对应ContentType(MIME)类型：audio/echospeech
        /// </summary>
        [Description("audio/echospeech")]
        _es,
        /// <summary>
        /// 扩展名.esl的文件，对应ContentType(MIME)类型：audio/echospeech
        /// </summary>
        [Description("audio/echospeech")]
        _esl,
        /// <summary>
        /// 扩展名.etc的文件，对应ContentType(MIME)类型：application/x-earthtime
        /// </summary>
        [Description("application/x-earthtime")]
        _etc,
        /// <summary>
        /// 扩展名.etx的文件，对应ContentType(MIME)类型：text/x-setext
        /// </summary>
        [Description("text/x-setext")]
        _etx,
        /// <summary>
        /// 扩展名.evm的文件，对应ContentType(MIME)类型：x-lml/x-evm
        /// </summary>
        [Description("x-lml/x-evm")]
        _evm,
        /// <summary>
        /// 扩展名.evy的文件，对应ContentType(MIME)类型：application/envoy
        /// </summary>
        [Description("application/envoy")]
        _evy,
        /// <summary>
        /// 扩展名.exe的文件，对应ContentType(MIME)类型：application/octet-stream
        /// </summary>
        [Description("application/octet-stream")]
        _exe,
        /// <summary>
        /// 扩展名.fh4的文件，对应ContentType(MIME)类型：image/x-freehand
        /// </summary>
        [Description("image/x-freehand")]
        _fh4,
        /// <summary>
        /// 扩展名.fh5的文件，对应ContentType(MIME)类型：image/x-freehand
        /// </summary>
        [Description("image/x-freehand")]
        _fh5,
        /// <summary>
        /// 扩展名.fhc的文件，对应ContentType(MIME)类型：image/x-freehand
        /// </summary>
        [Description("image/x-freehand")]
        _fhc,
        /// <summary>
        /// 扩展名.fif的文件，对应ContentType(MIME)类型：application/fractals
        /// </summary>
        [Description("application/fractals")]
        _fif,
        /// <summary>
        /// 扩展名.flr的文件，对应ContentType(MIME)类型：x-world/x-vrml
        /// </summary>
        [Description("x-world/x-vrml")]
        _flr,
        /// <summary>
        /// 扩展名.flv的文件，对应ContentType(MIME)类型：flv-application/octet-stream
        /// </summary>
        [Description("flv-application/octet-stream")]
        _flv,
        /// <summary>
        /// 扩展名.fm的文件，对应ContentType(MIME)类型：application/x-maker
        /// </summary>
        [Description("application/x-maker")]
        _fm,
        /// <summary>
        /// 扩展名.fpx的文件，对应ContentType(MIME)类型：image/x-fpx
        /// </summary>
        [Description("image/x-fpx")]
        _fpx,
        /// <summary>
        /// 扩展名.fvi的文件，对应ContentType(MIME)类型：video/isivideo
        /// </summary>
        [Description("video/isivideo")]
        _fvi,
        /// <summary>
        /// 扩展名.gau的文件，对应ContentType(MIME)类型：chemical/x-gaussian-input
        /// </summary>
        [Description("chemical/x-gaussian-input")]
        _gau,
        /// <summary>
        /// 扩展名.gca的文件，对应ContentType(MIME)类型：application/x-gca-compressed
        /// </summary>
        [Description("application/x-gca-compressed")]
        _gca,
        /// <summary>
        /// 扩展名.gdb的文件，对应ContentType(MIME)类型：x-lml/x-gdb
        /// </summary>
        [Description("x-lml/x-gdb")]
        _gdb,
        /// <summary>
        /// 扩展名.gif的文件，对应ContentType(MIME)类型：image/gif
        /// </summary>
        [Description("image/gif")]
        _gif,
        /// <summary>
        /// 扩展名.gps的文件，对应ContentType(MIME)类型：application/x-gps
        /// </summary>
        [Description("application/x-gps")]
        _gps,
        /// <summary>
        /// 扩展名.gtar的文件，对应ContentType(MIME)类型：application/x-gtar
        /// </summary>
        [Description("application/x-gtar")]
        _gtar,
        /// <summary>
        /// 扩展名.gz的文件，对应ContentType(MIME)类型：application/x-gzip
        /// </summary>
        [Description("application/x-gzip")]
        _gz,
        /// <summary>
        /// 扩展名.h的文件，对应ContentType(MIME)类型：text/plain
        /// </summary>
        [Description("text/plain")]
        _h,
        /// <summary>
        /// 扩展名.hdf的文件，对应ContentType(MIME)类型：application/x-hdf
        /// </summary>
        [Description("application/x-hdf")]
        _hdf,
        /// <summary>
        /// 扩展名.hdm的文件，对应ContentType(MIME)类型：text/x-hdml
        /// </summary>
        [Description("text/x-hdml")]
        _hdm,
        /// <summary>
        /// 扩展名.hdml的文件，对应ContentType(MIME)类型：text/x-hdml
        /// </summary>
        [Description("text/x-hdml")]
        _hdml,
        /// <summary>
        /// 扩展名.hlp的文件，对应ContentType(MIME)类型：application/winhlp
        /// </summary>
        [Description("application/winhlp")]
        _hlp,
        /// <summary>
        /// 扩展名.hqx的文件，对应ContentType(MIME)类型：application/mac-binhex40
        /// </summary>
        [Description("application/mac-binhex40")]
        _hqx,
        /// <summary>
        /// 扩展名.hta的文件，对应ContentType(MIME)类型：application/hta
        /// </summary>
        [Description("application/hta")]
        _hta,
        /// <summary>
        /// 扩展名.htc的文件，对应ContentType(MIME)类型：text/x-component
        /// </summary>
        [Description("text/x-component")]
        _htc,
        /// <summary>
        /// 扩展名.htm的文件，对应ContentType(MIME)类型：text/html
        /// </summary>
        [Description("text/html")]
        _htm,
        /// <summary>
        /// 扩展名.html的文件，对应ContentType(MIME)类型：text/html
        /// </summary>
        [Description("text/html")]
        _html,
        /// <summary>
        /// 扩展名.hts的文件，对应ContentType(MIME)类型：text/html
        /// </summary>
        [Description("text/html")]
        _hts,
        /// <summary>
        /// 扩展名.htt的文件，对应ContentType(MIME)类型：text/webviewhtml
        /// </summary>
        [Description("text/webviewhtml")]
        _htt,
        /// <summary>
        /// 扩展名.ice的文件，对应ContentType(MIME)类型：x-conference/x-cooltalk
        /// </summary>
        [Description("x-conference/x-cooltalk")]
        _ice,
        /// <summary>
        /// 扩展名.ico的文件，对应ContentType(MIME)类型：image/x-icon
        /// </summary>
        [Description("image/x-icon")]
        _ico,
        /// <summary>
        /// 扩展名.ief的文件，对应ContentType(MIME)类型：image/ief
        /// </summary>
        [Description("image/ief")]
        _ief,
        /// <summary>
        /// 扩展名.ifm的文件，对应ContentType(MIME)类型：image/gif
        /// </summary>
        [Description("image/gif")]
        _ifm,
        /// <summary>
        /// 扩展名.ifs的文件，对应ContentType(MIME)类型：image/ifs
        /// </summary>
        [Description("image/ifs")]
        _ifs,
        /// <summary>
        /// 扩展名.iii的文件，对应ContentType(MIME)类型：application/x-iphone
        /// </summary>
        [Description("application/x-iphone")]
        _iii,
        /// <summary>
        /// 扩展名.imy的文件，对应ContentType(MIME)类型：audio/melody
        /// </summary>
        [Description("audio/melody")]
        _imy,
        /// <summary>
        /// 扩展名.ins的文件，对应ContentType(MIME)类型：application/x-internet-signup
        /// </summary>
        [Description("application/x-internet-signup")]
        _ins,
        /// <summary>
        /// 扩展名.ips的文件，对应ContentType(MIME)类型：application/x-ipscript
        /// </summary>
        [Description("application/x-ipscript")]
        _ips,
        /// <summary>
        /// 扩展名.ipx的文件，对应ContentType(MIME)类型：application/x-ipix
        /// </summary>
        [Description("application/x-ipix")]
        _ipx,
        /// <summary>
        /// 扩展名.isp的文件，对应ContentType(MIME)类型：application/x-internet-signup
        /// </summary>
        [Description("application/x-internet-signup")]
        _isp,
        /// <summary>
        /// 扩展名.it的文件，对应ContentType(MIME)类型：audio/x-mod
        /// </summary>
        [Description("audio/x-mod")]
        _it,
        /// <summary>
        /// 扩展名.itz的文件，对应ContentType(MIME)类型：audio/x-mod
        /// </summary>
        [Description("audio/x-mod")]
        _itz,
        /// <summary>
        /// 扩展名.ivr的文件，对应ContentType(MIME)类型：i-world/i-vrml
        /// </summary>
        [Description("i-world/i-vrml")]
        _ivr,
        /// <summary>
        /// 扩展名.j2k的文件，对应ContentType(MIME)类型：image/j2k
        /// </summary>
        [Description("image/j2k")]
        _j2k,
        /// <summary>
        /// 扩展名.jad的文件，对应ContentType(MIME)类型：text/vnd.sun.j2me.app-descriptor
        /// </summary>
        [Description("text/vnd.sun.j2me.app-descriptor")]
        _jad,
        /// <summary>
        /// 扩展名.jam的文件，对应ContentType(MIME)类型：application/x-jam
        /// </summary>
        [Description("application/x-jam")]
        _jam,
        /// <summary>
        /// 扩展名.jar的文件，对应ContentType(MIME)类型：application/java-archive
        /// </summary>
        [Description("application/java-archive")]
        _jar,
        /// <summary>
        /// 扩展名.java的文件，对应ContentType(MIME)类型：text/plain
        /// </summary>
        [Description("text/plain")]
        _java,
        /// <summary>
        /// 扩展名.jfif的文件，对应ContentType(MIME)类型：image/pipeg
        /// </summary>
        [Description("image/pipeg")]
        _jfif,
        /// <summary>
        /// 扩展名.jnlp的文件，对应ContentType(MIME)类型：application/x-java-jnlp-file
        /// </summary>
        [Description("application/x-java-jnlp-file")]
        _jnlp,
        /// <summary>
        /// 扩展名.jpe的文件，对应ContentType(MIME)类型：image/jpeg
        /// </summary>
        [Description("image/jpeg")]
        _jpe,
        /// <summary>
        /// 扩展名.jpeg的文件，对应ContentType(MIME)类型：image/jpeg
        /// </summary>
        [Description("image/jpeg")]
        _jpeg,
        /// <summary>
        /// 扩展名.jpg的文件，对应ContentType(MIME)类型：image/jpeg
        /// </summary>
        [Description("image/jpeg")]
        _jpg,
        /// <summary>
        /// 扩展名.jpz的文件，对应ContentType(MIME)类型：image/jpeg
        /// </summary>
        [Description("image/jpeg")]
        _jpz,
        /// <summary>
        /// 扩展名.js的文件，对应ContentType(MIME)类型：application/x-javascript
        /// </summary>
        [Description("application/x-javascript")]
        _js,
        /// <summary>
        /// 扩展名.jwc的文件，对应ContentType(MIME)类型：application/jwc
        /// </summary>
        [Description("application/jwc")]
        _jwc,
        /// <summary>
        /// 扩展名.kjx的文件，对应ContentType(MIME)类型：application/x-kjx
        /// </summary>
        [Description("application/x-kjx")]
        _kjx,
        /// <summary>
        /// 扩展名.lak的文件，对应ContentType(MIME)类型：x-lml/x-lak
        /// </summary>
        [Description("x-lml/x-lak")]
        _lak,
        /// <summary>
        /// 扩展名.latex的文件，对应ContentType(MIME)类型：application/x-latex
        /// </summary>
        [Description("application/x-latex")]
        _latex,
        /// <summary>
        /// 扩展名.lcc的文件，对应ContentType(MIME)类型：application/fastman
        /// </summary>
        [Description("application/fastman")]
        _lcc,
        /// <summary>
        /// 扩展名.lcl的文件，对应ContentType(MIME)类型：application/x-digitalloca
        /// </summary>
        [Description("application/x-digitalloca")]
        _lcl,
        /// <summary>
        /// 扩展名.lcr的文件，对应ContentType(MIME)类型：application/x-digitalloca
        /// </summary>
        [Description("application/x-digitalloca")]
        _lcr,
        /// <summary>
        /// 扩展名.lgh的文件，对应ContentType(MIME)类型：application/lgh
        /// </summary>
        [Description("application/lgh")]
        _lgh,
        /// <summary>
        /// 扩展名.lha的文件，对应ContentType(MIME)类型：application/octet-stream
        /// </summary>
        [Description("application/octet-stream")]
        _lha,
        /// <summary>
        /// 扩展名.lml的文件，对应ContentType(MIME)类型：x-lml/x-lml
        /// </summary>
        [Description("x-lml/x-lml")]
        _lml,
        /// <summary>
        /// 扩展名.lmlpack的文件，对应ContentType(MIME)类型：x-lml/x-lmlpack
        /// </summary>
        [Description("x-lml/x-lmlpack")]
        _lmlpack,
        /// <summary>
        /// 扩展名.log的文件，对应ContentType(MIME)类型：text/plain
        /// </summary>
        [Description("text/plain")]
        _log,
        /// <summary>
        /// 扩展名.lsf的文件，对应ContentType(MIME)类型：video/x-la-asf
        /// </summary>
        [Description("video/x-la-asf")]
        _lsf,
        /// <summary>
        /// 扩展名.lsx的文件，对应ContentType(MIME)类型：video/x-la-asf
        /// </summary>
        [Description("video/x-la-asf")]
        _lsx,
        /// <summary>
        /// 扩展名.lzh的文件，对应ContentType(MIME)类型：application/octet-stream
        /// </summary>
        [Description("application/octet-stream")]
        _lzh,
        /// <summary>
        /// 扩展名.m13的文件，对应ContentType(MIME)类型：application/x-msmediaview
        /// </summary>
        [Description("application/x-msmediaview")]
        _m13,
        /// <summary>
        /// 扩展名.m14的文件，对应ContentType(MIME)类型：application/x-msmediaview
        /// </summary>
        [Description("application/x-msmediaview")]
        _m14,
        /// <summary>
        /// 扩展名.m15的文件，对应ContentType(MIME)类型：audio/x-mod
        /// </summary>
        [Description("audio/x-mod")]
        _m15,
        /// <summary>
        /// 扩展名.m3u的文件，对应ContentType(MIME)类型：audio/x-mpegurl
        /// </summary>
        [Description("audio/x-mpegurl")]
        _m3u,
        /// <summary>
        /// 扩展名.m3url的文件，对应ContentType(MIME)类型：audio/x-mpegurl
        /// </summary>
        [Description("audio/x-mpegurl")]
        _m3url,
        /// <summary>
        /// 扩展名.m4a的文件，对应ContentType(MIME)类型：audio/mp4a-latm
        /// </summary>
        [Description("audio/mp4a-latm")]
        _m4a,
        /// <summary>
        /// 扩展名.m4b的文件，对应ContentType(MIME)类型：audio/mp4a-latm
        /// </summary>
        [Description("audio/mp4a-latm")]
        _m4b,
        /// <summary>
        /// 扩展名.m4p的文件，对应ContentType(MIME)类型：audio/mp4a-latm
        /// </summary>
        [Description("audio/mp4a-latm")]
        _m4p,
        /// <summary>
        /// 扩展名.m4u的文件，对应ContentType(MIME)类型：video/vnd.mpegurl
        /// </summary>
        [Description("video/vnd.mpegurl")]
        _m4u,
        /// <summary>
        /// 扩展名.m4v的文件，对应ContentType(MIME)类型：video/x-m4v
        /// </summary>
        [Description("video/x-m4v")]
        _m4v,
        /// <summary>
        /// 扩展名.ma1的文件，对应ContentType(MIME)类型：audio/ma1
        /// </summary>
        [Description("audio/ma1")]
        _ma1,
        /// <summary>
        /// 扩展名.ma2的文件，对应ContentType(MIME)类型：audio/ma2
        /// </summary>
        [Description("audio/ma2")]
        _ma2,
        /// <summary>
        /// 扩展名.ma3的文件，对应ContentType(MIME)类型：audio/ma3
        /// </summary>
        [Description("audio/ma3")]
        _ma3,
        /// <summary>
        /// 扩展名.ma5的文件，对应ContentType(MIME)类型：audio/ma5
        /// </summary>
        [Description("audio/ma5")]
        _ma5,
        /// <summary>
        /// 扩展名.man的文件，对应ContentType(MIME)类型：application/x-troff-man
        /// </summary>
        [Description("application/x-troff-man")]
        _man,
        /// <summary>
        /// 扩展名.map的文件，对应ContentType(MIME)类型：magnus-internal/imagemap
        /// </summary>
        [Description("magnus-internal/imagemap")]
        _map,
        /// <summary>
        /// 扩展名.mbd的文件，对应ContentType(MIME)类型：application/mbedlet
        /// </summary>
        [Description("application/mbedlet")]
        _mbd,
        /// <summary>
        /// 扩展名.mct的文件，对应ContentType(MIME)类型：application/x-mascot
        /// </summary>
        [Description("application/x-mascot")]
        _mct,
        /// <summary>
        /// 扩展名.mdb的文件，对应ContentType(MIME)类型：application/x-msaccess
        /// </summary>
        [Description("application/x-msaccess")]
        _mdb,
        /// <summary>
        /// 扩展名.mdz的文件，对应ContentType(MIME)类型：audio/x-mod
        /// </summary>
        [Description("audio/x-mod")]
        _mdz,
        /// <summary>
        /// 扩展名.me的文件，对应ContentType(MIME)类型：application/x-troff-me
        /// </summary>
        [Description("application/x-troff-me")]
        _me,
        /// <summary>
        /// 扩展名.mel的文件，对应ContentType(MIME)类型：text/x-vmel
        /// </summary>
        [Description("text/x-vmel")]
        _mel,
        /// <summary>
        /// 扩展名.mht的文件，对应ContentType(MIME)类型：message/rfc822
        /// </summary>
        [Description("message/rfc822")]
        _mht,
        /// <summary>
        /// 扩展名.mhtml的文件，对应ContentType(MIME)类型：message/rfc822
        /// </summary>
        [Description("message/rfc822")]
        _mhtml,
        /// <summary>
        /// 扩展名.mi的文件，对应ContentType(MIME)类型：application/x-mif
        /// </summary>
        [Description("application/x-mif")]
        _mi,
        /// <summary>
        /// 扩展名.mid的文件，对应ContentType(MIME)类型：audio/mid
        /// </summary>
        [Description("audio/mid")]
        _mid,
        /// <summary>
        /// 扩展名.midi的文件，对应ContentType(MIME)类型：audio/midi
        /// </summary>
        [Description("audio/midi")]
        _midi,
        /// <summary>
        /// 扩展名.mif的文件，对应ContentType(MIME)类型：application/x-mif
        /// </summary>
        [Description("application/x-mif")]
        _mif,
        /// <summary>
        /// 扩展名.mil的文件，对应ContentType(MIME)类型：image/x-cals
        /// </summary>
        [Description("image/x-cals")]
        _mil,
        /// <summary>
        /// 扩展名.mio的文件，对应ContentType(MIME)类型：audio/x-mio
        /// </summary>
        [Description("audio/x-mio")]
        _mio,
        /// <summary>
        /// 扩展名.mmf的文件，对应ContentType(MIME)类型：application/x-skt-lbs
        /// </summary>
        [Description("application/x-skt-lbs")]
        _mmf,
        /// <summary>
        /// 扩展名.mng的文件，对应ContentType(MIME)类型：video/x-mng
        /// </summary>
        [Description("video/x-mng")]
        _mng,
        /// <summary>
        /// 扩展名.mny的文件，对应ContentType(MIME)类型：application/x-msmoney
        /// </summary>
        [Description("application/x-msmoney")]
        _mny,
        /// <summary>
        /// 扩展名.moc的文件，对应ContentType(MIME)类型：application/x-mocha
        /// </summary>
        [Description("application/x-mocha")]
        _moc,
        /// <summary>
        /// 扩展名.mocha的文件，对应ContentType(MIME)类型：application/x-mocha
        /// </summary>
        [Description("application/x-mocha")]
        _mocha,
        /// <summary>
        /// 扩展名.mod的文件，对应ContentType(MIME)类型：audio/x-mod
        /// </summary>
        [Description("audio/x-mod")]
        _mod,
        /// <summary>
        /// 扩展名.mof的文件，对应ContentType(MIME)类型：application/x-yumekara
        /// </summary>
        [Description("application/x-yumekara")]
        _mof,
        /// <summary>
        /// 扩展名.mol的文件，对应ContentType(MIME)类型：chemical/x-mdl-molfile
        /// </summary>
        [Description("chemical/x-mdl-molfile")]
        _mol,
        /// <summary>
        /// 扩展名.mop的文件，对应ContentType(MIME)类型：chemical/x-mopac-input
        /// </summary>
        [Description("chemical/x-mopac-input")]
        _mop,
        /// <summary>
        /// 扩展名.mov的文件，对应ContentType(MIME)类型：video/quicktime
        /// </summary>
        [Description("video/quicktime")]
        _mov,
        /// <summary>
        /// 扩展名.movie的文件，对应ContentType(MIME)类型：video/x-sgi-movie
        /// </summary>
        [Description("video/x-sgi-movie")]
        _movie,
        /// <summary>
        /// 扩展名.mp2的文件，对应ContentType(MIME)类型：video/mpeg
        /// </summary>
        [Description("video/mpeg")]
        _mp2,
        /// <summary>
        /// 扩展名.mp3的文件，对应ContentType(MIME)类型：audio/mpeg
        /// </summary>
        [Description("audio/mpeg")]
        _mp3,
        /// <summary>
        /// 扩展名.mp4的文件，对应ContentType(MIME)类型：video/mp4
        /// </summary>
        [Description("video/mp4")]
        _mp4,
        /// <summary>
        /// 扩展名.mpa的文件，对应ContentType(MIME)类型：video/mpeg
        /// </summary>
        [Description("video/mpeg")]
        _mpa,
        /// <summary>
        /// 扩展名.mpc的文件，对应ContentType(MIME)类型：application/vnd.mpohun.certificate
        /// </summary>
        [Description("application/vnd.mpohun.certificate")]
        _mpc,
        /// <summary>
        /// 扩展名.mpe的文件，对应ContentType(MIME)类型：video/mpeg
        /// </summary>
        [Description("video/mpeg")]
        _mpe,
        /// <summary>
        /// 扩展名.mpeg的文件，对应ContentType(MIME)类型：video/mpeg
        /// </summary>
        [Description("video/mpeg")]
        _mpeg,
        /// <summary>
        /// 扩展名.mpg的文件，对应ContentType(MIME)类型：video/mpeg
        /// </summary>
        [Description("video/mpeg")]
        _mpg,
        /// <summary>
        /// 扩展名.mpg4的文件，对应ContentType(MIME)类型：video/mp4
        /// </summary>
        [Description("video/mp4")]
        _mpg4,
        /// <summary>
        /// 扩展名.mpga的文件，对应ContentType(MIME)类型：audio/mpeg
        /// </summary>
        [Description("audio/mpeg")]
        _mpga,
        /// <summary>
        /// 扩展名.mpn的文件，对应ContentType(MIME)类型：application/vnd.mophun.application
        /// </summary>
        [Description("application/vnd.mophun.application")]
        _mpn,
        /// <summary>
        /// 扩展名.mpp的文件，对应ContentType(MIME)类型：application/vnd.ms-project
        /// </summary>
        [Description("application/vnd.ms-project")]
        _mpp,
        /// <summary>
        /// 扩展名.mps的文件，对应ContentType(MIME)类型：application/x-mapserver
        /// </summary>
        [Description("application/x-mapserver")]
        _mps,
        /// <summary>
        /// 扩展名.mpv2的文件，对应ContentType(MIME)类型：video/mpeg
        /// </summary>
        [Description("video/mpeg")]
        _mpv2,
        /// <summary>
        /// 扩展名.mrl的文件，对应ContentType(MIME)类型：text/x-mrml
        /// </summary>
        [Description("text/x-mrml")]
        _mrl,
        /// <summary>
        /// 扩展名.mrm的文件，对应ContentType(MIME)类型：application/x-mrm
        /// </summary>
        [Description("application/x-mrm")]
        _mrm,
        /// <summary>
        /// 扩展名.ms的文件，对应ContentType(MIME)类型：application/x-troff-ms
        /// </summary>
        [Description("application/x-troff-ms")]
        _ms,
        /// <summary>
        /// 扩展名.msg的文件，对应ContentType(MIME)类型：application/vnd.ms-outlook
        /// </summary>
        [Description("application/vnd.ms-outlook")]
        _msg,
        /// <summary>
        /// 扩展名.mts的文件，对应ContentType(MIME)类型：application/metastream
        /// </summary>
        [Description("application/metastream")]
        _mts,
        /// <summary>
        /// 扩展名.mtx的文件，对应ContentType(MIME)类型：application/metastream
        /// </summary>
        [Description("application/metastream")]
        _mtx,
        /// <summary>
        /// 扩展名.mtz的文件，对应ContentType(MIME)类型：application/metastream
        /// </summary>
        [Description("application/metastream")]
        _mtz,
        /// <summary>
        /// 扩展名.mvb的文件，对应ContentType(MIME)类型：application/x-msmediaview
        /// </summary>
        [Description("application/x-msmediaview")]
        _mvb,
        /// <summary>
        /// 扩展名.mzv的文件，对应ContentType(MIME)类型：application/metastream
        /// </summary>
        [Description("application/metastream")]
        _mzv,
        /// <summary>
        /// 扩展名.nar的文件，对应ContentType(MIME)类型：application/zip
        /// </summary>
        [Description("application/zip")]
        _nar,
        /// <summary>
        /// 扩展名.nbmp的文件，对应ContentType(MIME)类型：image/nbmp
        /// </summary>
        [Description("image/nbmp")]
        _nbmp,
        /// <summary>
        /// 扩展名.nc的文件，对应ContentType(MIME)类型：application/x-netcdf
        /// </summary>
        [Description("application/x-netcdf")]
        _nc,
        /// <summary>
        /// 扩展名.ndb的文件，对应ContentType(MIME)类型：x-lml/x-ndb
        /// </summary>
        [Description("x-lml/x-ndb")]
        _ndb,
        /// <summary>
        /// 扩展名.ndwn的文件，对应ContentType(MIME)类型：application/ndwn
        /// </summary>
        [Description("application/ndwn")]
        _ndwn,
        /// <summary>
        /// 扩展名.nif的文件，对应ContentType(MIME)类型：application/x-nif
        /// </summary>
        [Description("application/x-nif")]
        _nif,
        /// <summary>
        /// 扩展名.nmz的文件，对应ContentType(MIME)类型：application/x-scream
        /// </summary>
        [Description("application/x-scream")]
        _nmz,
        /// <summary>
        /// 扩展名.npx的文件，对应ContentType(MIME)类型：application/x-netfpx
        /// </summary>
        [Description("application/x-netfpx")]
        _npx,
        /// <summary>
        /// 扩展名.nsnd的文件，对应ContentType(MIME)类型：audio/nsnd
        /// </summary>
        [Description("audio/nsnd")]
        _nsnd,
        /// <summary>
        /// 扩展名.nva的文件，对应ContentType(MIME)类型：application/x-neva1
        /// </summary>
        [Description("application/x-neva1")]
        _nva,
        /// <summary>
        /// 扩展名.nws的文件，对应ContentType(MIME)类型：message/rfc822
        /// </summary>
        [Description("message/rfc822")]
        _nws,
        /// <summary>
        /// 扩展名.oda的文件，对应ContentType(MIME)类型：application/oda
        /// </summary>
        [Description("application/oda")]
        _oda,
        /// <summary>
        /// 扩展名.ogg的文件，对应ContentType(MIME)类型：audio/ogg
        /// </summary>
        [Description("audio/ogg")]
        _ogg,
        /// <summary>
        /// 扩展名.oom的文件，对应ContentType(MIME)类型：application/x-AtlasMate-Plugin
        /// </summary>
        [Description("application/x-AtlasMate-Plugin")]
        _oom,
        /// <summary>
        /// 扩展名.p10的文件，对应ContentType(MIME)类型：application/pkcs10
        /// </summary>
        [Description("application/pkcs10")]
        _p10,
        /// <summary>
        /// 扩展名.p12的文件，对应ContentType(MIME)类型：application/x-pkcs12
        /// </summary>
        [Description("application/x-pkcs12")]
        _p12,
        /// <summary>
        /// 扩展名.p7b的文件，对应ContentType(MIME)类型：application/x-pkcs7-certificates
        /// </summary>
        [Description("application/x-pkcs7-certificates")]
        _p7b,
        /// <summary>
        /// 扩展名.p7c的文件，对应ContentType(MIME)类型：application/x-pkcs7-mime
        /// </summary>
        [Description("application/x-pkcs7-mime")]
        _p7c,
        /// <summary>
        /// 扩展名.p7m的文件，对应ContentType(MIME)类型：application/x-pkcs7-mime
        /// </summary>
        [Description("application/x-pkcs7-mime")]
        _p7m,
        /// <summary>
        /// 扩展名.p7r的文件，对应ContentType(MIME)类型：application/x-pkcs7-certreqresp
        /// </summary>
        [Description("application/x-pkcs7-certreqresp")]
        _p7r,
        /// <summary>
        /// 扩展名.p7s的文件，对应ContentType(MIME)类型：application/x-pkcs7-signature
        /// </summary>
        [Description("application/x-pkcs7-signature")]
        _p7s,
        /// <summary>
        /// 扩展名.pac的文件，对应ContentType(MIME)类型：audio/x-pac
        /// </summary>
        [Description("audio/x-pac")]
        _pac,
        /// <summary>
        /// 扩展名.pae的文件，对应ContentType(MIME)类型：audio/x-epac
        /// </summary>
        [Description("audio/x-epac")]
        _pae,
        /// <summary>
        /// 扩展名.pan的文件，对应ContentType(MIME)类型：application/x-pan
        /// </summary>
        [Description("application/x-pan")]
        _pan,
        /// <summary>
        /// 扩展名.pbm的文件，对应ContentType(MIME)类型：image/x-portable-bitmap
        /// </summary>
        [Description("image/x-portable-bitmap")]
        _pbm,
        /// <summary>
        /// 扩展名.pcx的文件，对应ContentType(MIME)类型：image/x-pcx
        /// </summary>
        [Description("image/x-pcx")]
        _pcx,
        /// <summary>
        /// 扩展名.pda的文件，对应ContentType(MIME)类型：image/x-pda
        /// </summary>
        [Description("image/x-pda")]
        _pda,
        /// <summary>
        /// 扩展名.pdb的文件，对应ContentType(MIME)类型：chemical/x-pdb
        /// </summary>
        [Description("chemical/x-pdb")]
        _pdb,
        /// <summary>
        /// 扩展名.pdf的文件，对应ContentType(MIME)类型：application/pdf
        /// </summary>
        [Description("application/pdf")]
        _pdf,
        /// <summary>
        /// 扩展名.pfr的文件，对应ContentType(MIME)类型：application/font-tdpfr
        /// </summary>
        [Description("application/font-tdpfr")]
        _pfr,
        /// <summary>
        /// 扩展名.pfx的文件，对应ContentType(MIME)类型：application/x-pkcs12
        /// </summary>
        [Description("application/x-pkcs12")]
        _pfx,
        /// <summary>
        /// 扩展名.pgm的文件，对应ContentType(MIME)类型：image/x-portable-graymap
        /// </summary>
        [Description("image/x-portable-graymap")]
        _pgm,
        /// <summary>
        /// 扩展名.pict的文件，对应ContentType(MIME)类型：image/x-pict
        /// </summary>
        [Description("image/x-pict")]
        _pict,
        /// <summary>
        /// 扩展名.pko的文件，对应ContentType(MIME)类型：application/ynd.ms-pkipko
        /// </summary>
        [Description("application/ynd.ms-pkipko")]
        _pko,
        /// <summary>
        /// 扩展名.pm的文件，对应ContentType(MIME)类型：application/x-perl
        /// </summary>
        [Description("application/x-perl")]
        _pm,
        /// <summary>
        /// 扩展名.pma的文件，对应ContentType(MIME)类型：application/x-perfmon
        /// </summary>
        [Description("application/x-perfmon")]
        _pma,
        /// <summary>
        /// 扩展名.pmc的文件，对应ContentType(MIME)类型：application/x-perfmon
        /// </summary>
        [Description("application/x-perfmon")]
        _pmc,
        /// <summary>
        /// 扩展名.pmd的文件，对应ContentType(MIME)类型：application/x-pmd
        /// </summary>
        [Description("application/x-pmd")]
        _pmd,
        /// <summary>
        /// 扩展名.pml的文件，对应ContentType(MIME)类型：application/x-perfmon
        /// </summary>
        [Description("application/x-perfmon")]
        _pml,
        /// <summary>
        /// 扩展名.pmr的文件，对应ContentType(MIME)类型：application/x-perfmon
        /// </summary>
        [Description("application/x-perfmon")]
        _pmr,
        /// <summary>
        /// 扩展名.pmw的文件，对应ContentType(MIME)类型：application/x-perfmon
        /// </summary>
        [Description("application/x-perfmon")]
        _pmw,
        /// <summary>
        /// 扩展名.png的文件，对应ContentType(MIME)类型：image/png
        /// </summary>
        [Description("image/png")]
        _png,
        /// <summary>
        /// 扩展名.pnm的文件，对应ContentType(MIME)类型：image/x-portable-anymap
        /// </summary>
        [Description("image/x-portable-anymap")]
        _pnm,
        /// <summary>
        /// 扩展名.pnz的文件，对应ContentType(MIME)类型：image/png
        /// </summary>
        [Description("image/png")]
        _pnz,
        /// <summary>
        /// 扩展名.ppm的文件，对应ContentType(MIME)类型：image/x-portable-pixmap
        /// </summary>
        [Description("image/x-portable-pixmap")]
        _ppm,
        /// <summary>
        /// 扩展名.pps的文件，对应ContentType(MIME)类型：application/vnd.ms-powerpoint
        /// </summary>
        [Description("application/vnd.ms-powerpoint")]
        _pps,
        /// <summary>
        /// 扩展名.ppt的文件，对应ContentType(MIME)类型：application/vnd.ms-powerpoint
        /// </summary>
        [Description("application/vnd.ms-powerpoint")]
        _ppt,
        /// <summary>
        /// 扩展名.pptx的文件，对应ContentType(MIME)类型：application/vnd.openxmlformats-officedocument.presentationml.presentation
        /// </summary>
        [Description("application/vnd.openxmlformats-officedocument.presentationml.presentation")]
        _pptx,
        /// <summary>
        /// 扩展名.pqf的文件，对应ContentType(MIME)类型：application/x-cprplayer
        /// </summary>
        [Description("application/x-cprplayer")]
        _pqf,
        /// <summary>
        /// 扩展名.pqi的文件，对应ContentType(MIME)类型：application/cprplayer
        /// </summary>
        [Description("application/cprplayer")]
        _pqi,
        /// <summary>
        /// 扩展名.prc的文件，对应ContentType(MIME)类型：application/x-prc
        /// </summary>
        [Description("application/x-prc")]
        _prc,
        /// <summary>
        /// 扩展名.prf的文件，对应ContentType(MIME)类型：application/pics-rules
        /// </summary>
        [Description("application/pics-rules")]
        _prf,
        /// <summary>
        /// 扩展名.prop的文件，对应ContentType(MIME)类型：text/plain
        /// </summary>
        [Description("text/plain")]
        _prop,
        /// <summary>
        /// 扩展名.proxy的文件，对应ContentType(MIME)类型：application/x-ns-proxy-autoconfig
        /// </summary>
        [Description("application/x-ns-proxy-autoconfig")]
        _proxy,
        /// <summary>
        /// 扩展名.ps的文件，对应ContentType(MIME)类型：application/postscript
        /// </summary>
        [Description("application/postscript")]
        _ps,
        /// <summary>
        /// 扩展名.ptlk的文件，对应ContentType(MIME)类型：application/listenup
        /// </summary>
        [Description("application/listenup")]
        _ptlk,
        /// <summary>
        /// 扩展名.pub的文件，对应ContentType(MIME)类型：application/x-mspublisher
        /// </summary>
        [Description("application/x-mspublisher")]
        _pub,
        /// <summary>
        /// 扩展名.pvx的文件，对应ContentType(MIME)类型：video/x-pv-pvx
        /// </summary>
        [Description("video/x-pv-pvx")]
        _pvx,
        /// <summary>
        /// 扩展名.qcp的文件，对应ContentType(MIME)类型：audio/vnd.qcelp
        /// </summary>
        [Description("audio/vnd.qcelp")]
        _qcp,
        /// <summary>
        /// 扩展名.qt的文件，对应ContentType(MIME)类型：video/quicktime
        /// </summary>
        [Description("video/quicktime")]
        _qt,
        /// <summary>
        /// 扩展名.qti的文件，对应ContentType(MIME)类型：image/x-quicktime
        /// </summary>
        [Description("image/x-quicktime")]
        _qti,
        /// <summary>
        /// 扩展名.qtif的文件，对应ContentType(MIME)类型：image/x-quicktime
        /// </summary>
        [Description("image/x-quicktime")]
        _qtif,
        /// <summary>
        /// 扩展名.r3t的文件，对应ContentType(MIME)类型：text/vnd.rn-realtext3d
        /// </summary>
        [Description("text/vnd.rn-realtext3d")]
        _r3t,
        /// <summary>
        /// 扩展名.ra的文件，对应ContentType(MIME)类型：audio/x-pn-realaudio
        /// </summary>
        [Description("audio/x-pn-realaudio")]
        _ra,
        /// <summary>
        /// 扩展名.ram的文件，对应ContentType(MIME)类型：audio/x-pn-realaudio
        /// </summary>
        [Description("audio/x-pn-realaudio")]
        _ram,
        /// <summary>
        /// 扩展名.rar的文件，对应ContentType(MIME)类型：application/octet-stream
        /// </summary>
        [Description("application/octet-stream")]
        _rar,
        /// <summary>
        /// 扩展名.ras的文件，对应ContentType(MIME)类型：image/x-cmu-raster
        /// </summary>
        [Description("image/x-cmu-raster")]
        _ras,
        /// <summary>
        /// 扩展名.rc的文件，对应ContentType(MIME)类型：text/plain
        /// </summary>
        [Description("text/plain")]
        _rc,
        /// <summary>
        /// 扩展名.rdf的文件，对应ContentType(MIME)类型：application/rdf+xml
        /// </summary>
        [Description("application/rdf+xml")]
        _rdf,
        /// <summary>
        /// 扩展名.rf的文件，对应ContentType(MIME)类型：image/vnd.rn-realflash
        /// </summary>
        [Description("image/vnd.rn-realflash")]
        _rf,
        /// <summary>
        /// 扩展名.rgb的文件，对应ContentType(MIME)类型：image/x-rgb
        /// </summary>
        [Description("image/x-rgb")]
        _rgb,
        /// <summary>
        /// 扩展名.rlf的文件，对应ContentType(MIME)类型：application/x-richlink
        /// </summary>
        [Description("application/x-richlink")]
        _rlf,
        /// <summary>
        /// 扩展名.rm的文件，对应ContentType(MIME)类型：audio/x-pn-realaudio
        /// </summary>
        [Description("audio/x-pn-realaudio")]
        _rm,
        /// <summary>
        /// 扩展名.rmf的文件，对应ContentType(MIME)类型：audio/x-rmf
        /// </summary>
        [Description("audio/x-rmf")]
        _rmf,
        /// <summary>
        /// 扩展名.rmi的文件，对应ContentType(MIME)类型：audio/mid
        /// </summary>
        [Description("audio/mid")]
        _rmi,
        /// <summary>
        /// 扩展名.rmm的文件，对应ContentType(MIME)类型：audio/x-pn-realaudio
        /// </summary>
        [Description("audio/x-pn-realaudio")]
        _rmm,
        /// <summary>
        /// 扩展名.rmvb的文件，对应ContentType(MIME)类型：audio/x-pn-realaudio
        /// </summary>
        [Description("audio/x-pn-realaudio")]
        _rmvb,
        /// <summary>
        /// 扩展名.rnx的文件，对应ContentType(MIME)类型：application/vnd.rn-realplayer
        /// </summary>
        [Description("application/vnd.rn-realplayer")]
        _rnx,
        /// <summary>
        /// 扩展名.roff的文件，对应ContentType(MIME)类型：application/x-troff
        /// </summary>
        [Description("application/x-troff")]
        _roff,
        /// <summary>
        /// 扩展名.rp的文件，对应ContentType(MIME)类型：image/vnd.rn-realpix
        /// </summary>
        [Description("image/vnd.rn-realpix")]
        _rp,
        /// <summary>
        /// 扩展名.rpm的文件，对应ContentType(MIME)类型：audio/x-pn-realaudio-plugin
        /// </summary>
        [Description("audio/x-pn-realaudio-plugin")]
        _rpm,
        /// <summary>
        /// 扩展名.rt的文件，对应ContentType(MIME)类型：text/vnd.rn-realtext
        /// </summary>
        [Description("text/vnd.rn-realtext")]
        _rt,
        /// <summary>
        /// 扩展名.rte的文件，对应ContentType(MIME)类型：x-lml/x-gps
        /// </summary>
        [Description("x-lml/x-gps")]
        _rte,
        /// <summary>
        /// 扩展名.rtf的文件，对应ContentType(MIME)类型：application/rtf
        /// </summary>
        [Description("application/rtf")]
        _rtf,
        /// <summary>
        /// 扩展名.rtg的文件，对应ContentType(MIME)类型：application/metastream
        /// </summary>
        [Description("application/metastream")]
        _rtg,
        /// <summary>
        /// 扩展名.rtx的文件，对应ContentType(MIME)类型：text/richtext
        /// </summary>
        [Description("text/richtext")]
        _rtx,
        /// <summary>
        /// 扩展名.rv的文件，对应ContentType(MIME)类型：video/vnd.rn-realvideo
        /// </summary>
        [Description("video/vnd.rn-realvideo")]
        _rv,
        /// <summary>
        /// 扩展名.rwc的文件，对应ContentType(MIME)类型：application/x-rogerwilco
        /// </summary>
        [Description("application/x-rogerwilco")]
        _rwc,
        /// <summary>
        /// 扩展名.s3m的文件，对应ContentType(MIME)类型：audio/x-mod
        /// </summary>
        [Description("audio/x-mod")]
        _s3m,
        /// <summary>
        /// 扩展名.s3z的文件，对应ContentType(MIME)类型：audio/x-mod
        /// </summary>
        [Description("audio/x-mod")]
        _s3z,
        /// <summary>
        /// 扩展名.sca的文件，对应ContentType(MIME)类型：application/x-supercard
        /// </summary>
        [Description("application/x-supercard")]
        _sca,
        /// <summary>
        /// 扩展名.scd的文件，对应ContentType(MIME)类型：application/x-msschedule
        /// </summary>
        [Description("application/x-msschedule")]
        _scd,
        /// <summary>
        /// 扩展名.sct的文件，对应ContentType(MIME)类型：text/scriptlet
        /// </summary>
        [Description("text/scriptlet")]
        _sct,
        /// <summary>
        /// 扩展名.sdf的文件，对应ContentType(MIME)类型：application/e-score
        /// </summary>
        [Description("application/e-score")]
        _sdf,
        /// <summary>
        /// 扩展名.sea的文件，对应ContentType(MIME)类型：application/x-stuffit
        /// </summary>
        [Description("application/x-stuffit")]
        _sea,
        /// <summary>
        /// 扩展名.setpay的文件，对应ContentType(MIME)类型：application/set-payment-initiation
        /// </summary>
        [Description("application/set-payment-initiation")]
        _setpay,
        /// <summary>
        /// 扩展名.setreg的文件，对应ContentType(MIME)类型：application/set-registration-initiation
        /// </summary>
        [Description("application/set-registration-initiation")]
        _setreg,
        /// <summary>
        /// 扩展名.sgm的文件，对应ContentType(MIME)类型：text/x-sgml
        /// </summary>
        [Description("text/x-sgml")]
        _sgm,
        /// <summary>
        /// 扩展名.sgml的文件，对应ContentType(MIME)类型：text/x-sgml
        /// </summary>
        [Description("text/x-sgml")]
        _sgml,
        /// <summary>
        /// 扩展名.sh的文件，对应ContentType(MIME)类型：application/x-sh
        /// </summary>
        [Description("application/x-sh")]
        _sh,
        /// <summary>
        /// 扩展名.shar的文件，对应ContentType(MIME)类型：application/x-shar
        /// </summary>
        [Description("application/x-shar")]
        _shar,
        /// <summary>
        /// 扩展名.shtml的文件，对应ContentType(MIME)类型：magnus-internal/parsed-html
        /// </summary>
        [Description("magnus-internal/parsed-html")]
        _shtml,
        /// <summary>
        /// 扩展名.shw的文件，对应ContentType(MIME)类型：application/presentations
        /// </summary>
        [Description("application/presentations")]
        _shw,
        /// <summary>
        /// 扩展名.si6的文件，对应ContentType(MIME)类型：image/si6
        /// </summary>
        [Description("image/si6")]
        _si6,
        /// <summary>
        /// 扩展名.si7的文件，对应ContentType(MIME)类型：image/vnd.stiwap.sis
        /// </summary>
        [Description("image/vnd.stiwap.sis")]
        _si7,
        /// <summary>
        /// 扩展名.si9的文件，对应ContentType(MIME)类型：image/vnd.lgtwap.sis
        /// </summary>
        [Description("image/vnd.lgtwap.sis")]
        _si9,
        /// <summary>
        /// 扩展名.sis的文件，对应ContentType(MIME)类型：application/vnd.symbian.install
        /// </summary>
        [Description("application/vnd.symbian.install")]
        _sis,
        /// <summary>
        /// 扩展名.sit的文件，对应ContentType(MIME)类型：application/x-stuffit
        /// </summary>
        [Description("application/x-stuffit")]
        _sit,
        /// <summary>
        /// 扩展名.skd的文件，对应ContentType(MIME)类型：application/x-Koan
        /// </summary>
        [Description("application/x-Koan")]
        _skd,
        /// <summary>
        /// 扩展名.skm的文件，对应ContentType(MIME)类型：application/x-Koan
        /// </summary>
        [Description("application/x-Koan")]
        _skm,
        /// <summary>
        /// 扩展名.skp的文件，对应ContentType(MIME)类型：application/x-Koan
        /// </summary>
        [Description("application/x-Koan")]
        _skp,
        /// <summary>
        /// 扩展名.skt的文件，对应ContentType(MIME)类型：application/x-Koan
        /// </summary>
        [Description("application/x-Koan")]
        _skt,
        /// <summary>
        /// 扩展名.slc的文件，对应ContentType(MIME)类型：application/x-salsa
        /// </summary>
        [Description("application/x-salsa")]
        _slc,
        /// <summary>
        /// 扩展名.smd的文件，对应ContentType(MIME)类型：audio/x-smd
        /// </summary>
        [Description("audio/x-smd")]
        _smd,
        /// <summary>
        /// 扩展名.smi的文件，对应ContentType(MIME)类型：application/smil
        /// </summary>
        [Description("application/smil")]
        _smi,
        /// <summary>
        /// 扩展名.smil的文件，对应ContentType(MIME)类型：application/smil
        /// </summary>
        [Description("application/smil")]
        _smil,
        /// <summary>
        /// 扩展名.smp的文件，对应ContentType(MIME)类型：application/studiom
        /// </summary>
        [Description("application/studiom")]
        _smp,
        /// <summary>
        /// 扩展名.smz的文件，对应ContentType(MIME)类型：audio/x-smd
        /// </summary>
        [Description("audio/x-smd")]
        _smz,
        /// <summary>
        /// 扩展名.snd的文件，对应ContentType(MIME)类型：audio/basic
        /// </summary>
        [Description("audio/basic")]
        _snd,
        /// <summary>
        /// 扩展名.spc的文件，对应ContentType(MIME)类型：application/x-pkcs7-certificates
        /// </summary>
        [Description("application/x-pkcs7-certificates")]
        _spc,
        /// <summary>
        /// 扩展名.spl的文件，对应ContentType(MIME)类型：application/futuresplash
        /// </summary>
        [Description("application/futuresplash")]
        _spl,
        /// <summary>
        /// 扩展名.spr的文件，对应ContentType(MIME)类型：application/x-sprite
        /// </summary>
        [Description("application/x-sprite")]
        _spr,
        /// <summary>
        /// 扩展名.sprite的文件，对应ContentType(MIME)类型：application/x-sprite
        /// </summary>
        [Description("application/x-sprite")]
        _sprite,
        /// <summary>
        /// 扩展名.sdp的文件，对应ContentType(MIME)类型：application/sdp
        /// </summary>
        [Description("application/sdp")]
        _sdp,
        /// <summary>
        /// 扩展名.spt的文件，对应ContentType(MIME)类型：application/x-spt
        /// </summary>
        [Description("application/x-spt")]
        _spt,
        /// <summary>
        /// 扩展名.src的文件，对应ContentType(MIME)类型：application/x-wais-source
        /// </summary>
        [Description("application/x-wais-source")]
        _src,
        /// <summary>
        /// 扩展名.sst的文件，对应ContentType(MIME)类型：application/vnd.ms-pkicertstore
        /// </summary>
        [Description("application/vnd.ms-pkicertstore")]
        _sst,
        /// <summary>
        /// 扩展名.stk的文件，对应ContentType(MIME)类型：application/hyperstudio
        /// </summary>
        [Description("application/hyperstudio")]
        _stk,
        /// <summary>
        /// 扩展名.stl的文件，对应ContentType(MIME)类型：application/vnd.ms-pkistl
        /// </summary>
        [Description("application/vnd.ms-pkistl")]
        _stl,
        /// <summary>
        /// 扩展名.stm的文件，对应ContentType(MIME)类型：text/html
        /// </summary>
        [Description("text/html")]
        _stm,
        /// <summary>
        /// 扩展名.svg的文件，对应ContentType(MIME)类型：image/svg+xml
        /// </summary>
        [Description("image/svg+xml")]
        _svg,
        /// <summary>
        /// 扩展名.sv4cpio的文件，对应ContentType(MIME)类型：application/x-sv4cpio
        /// </summary>
        [Description("application/x-sv4cpio")]
        _sv4cpio,
        /// <summary>
        /// 扩展名.sv4crc的文件，对应ContentType(MIME)类型：application/x-sv4crc
        /// </summary>
        [Description("application/x-sv4crc")]
        _sv4crc,
        /// <summary>
        /// 扩展名.svf的文件，对应ContentType(MIME)类型：image/vnd
        /// </summary>
        [Description("image/vnd")]
        _svf,
        ///// <summary>
        ///// 扩展名.svg的文件，对应ContentType(MIME)类型：image/svg+xml
        ///// </summary>
        //[Description("image/svg+xml")]
        //_svg,
        /// <summary>
        /// 扩展名.svh的文件，对应ContentType(MIME)类型：image/svh
        /// </summary>
        [Description("image/svh")]
        _svh,
        /// <summary>
        /// 扩展名.svr的文件，对应ContentType(MIME)类型：x-world/x-svr
        /// </summary>
        [Description("x-world/x-svr")]
        _svr,
        /// <summary>
        /// 扩展名.swf的文件，对应ContentType(MIME)类型：application/x-shockwave-flash
        /// </summary>
        [Description("application/x-shockwave-flash")]
        _swf,
        /// <summary>
        /// 扩展名.swfl的文件，对应ContentType(MIME)类型：application/x-shockwave-flash
        /// </summary>
        [Description("application/x-shockwave-flash")]
        _swfl,
        /// <summary>
        /// 扩展名.t的文件，对应ContentType(MIME)类型：application/x-troff
        /// </summary>
        [Description("application/x-troff")]
        _t,
        /// <summary>
        /// 扩展名.tad的文件，对应ContentType(MIME)类型：application/octet-stream
        /// </summary>
        [Description("application/octet-stream")]
        _tad,
        /// <summary>
        /// 扩展名.talk的文件，对应ContentType(MIME)类型：text/x-speech
        /// </summary>
        [Description("text/x-speech")]
        _talk,
        /// <summary>
        /// 扩展名.tar的文件，对应ContentType(MIME)类型：application/x-tar
        /// </summary>
        [Description("application/x-tar")]
        _tar,
        /// <summary>
        /// 扩展名.taz的文件，对应ContentType(MIME)类型：application/x-tar
        /// </summary>
        [Description("application/x-tar")]
        _taz,
        /// <summary>
        /// 扩展名.tbp的文件，对应ContentType(MIME)类型：application/x-timbuktu
        /// </summary>
        [Description("application/x-timbuktu")]
        _tbp,
        /// <summary>
        /// 扩展名.tbt的文件，对应ContentType(MIME)类型：application/x-timbuktu
        /// </summary>
        [Description("application/x-timbuktu")]
        _tbt,
        /// <summary>
        /// 扩展名.tcl的文件，对应ContentType(MIME)类型：application/x-tcl
        /// </summary>
        [Description("application/x-tcl")]
        _tcl,
        /// <summary>
        /// 扩展名.tex的文件，对应ContentType(MIME)类型：application/x-tex
        /// </summary>
        [Description("application/x-tex")]
        _tex,
        /// <summary>
        /// 扩展名.texi的文件，对应ContentType(MIME)类型：application/x-texinfo
        /// </summary>
        [Description("application/x-texinfo")]
        _texi,
        /// <summary>
        /// 扩展名.texinfo的文件，对应ContentType(MIME)类型：application/x-texinfo
        /// </summary>
        [Description("application/x-texinfo")]
        _texinfo,
        /// <summary>
        /// 扩展名.tgz的文件，对应ContentType(MIME)类型：application/x-compressed
        /// </summary>
        [Description("application/x-compressed")]
        _tgz,
        /// <summary>
        /// 扩展名.thm的文件，对应ContentType(MIME)类型：application/vnd.eri.thm
        /// </summary>
        [Description("application/vnd.eri.thm")]
        _thm,
        /// <summary>
        /// 扩展名.tif的文件，对应ContentType(MIME)类型：image/tiff
        /// </summary>
        [Description("image/tiff")]
        _tif,
        /// <summary>
        /// 扩展名.tiff的文件，对应ContentType(MIME)类型：image/tiff
        /// </summary>
        [Description("image/tiff")]
        _tiff,
        /// <summary>
        /// 扩展名.tki的文件，对应ContentType(MIME)类型：application/x-tkined
        /// </summary>
        [Description("application/x-tkined")]
        _tki,
        /// <summary>
        /// 扩展名.tkined的文件，对应ContentType(MIME)类型：application/x-tkined
        /// </summary>
        [Description("application/x-tkined")]
        _tkined,
        /// <summary>
        /// 扩展名.toc的文件，对应ContentType(MIME)类型：application/toc
        /// </summary>
        [Description("application/toc")]
        _toc,
        /// <summary>
        /// 扩展名.toy的文件，对应ContentType(MIME)类型：image/toy
        /// </summary>
        [Description("image/toy")]
        _toy,
        /// <summary>
        /// 扩展名.tr的文件，对应ContentType(MIME)类型：application/x-troff
        /// </summary>
        [Description("application/x-troff")]
        _tr,
        /// <summary>
        /// 扩展名.trk的文件，对应ContentType(MIME)类型：x-lml/x-gps
        /// </summary>
        [Description("x-lml/x-gps")]
        _trk,
        /// <summary>
        /// 扩展名.trm的文件，对应ContentType(MIME)类型：application/x-msterminal
        /// </summary>
        [Description("application/x-msterminal")]
        _trm,
        /// <summary>
        /// 扩展名.tsi的文件，对应ContentType(MIME)类型：audio/tsplayer
        /// </summary>
        [Description("audio/tsplayer")]
        _tsi,
        /// <summary>
        /// 扩展名.tsp的文件，对应ContentType(MIME)类型：application/dsptype
        /// </summary>
        [Description("application/dsptype")]
        _tsp,
        /// <summary>
        /// 扩展名.tsv的文件，对应ContentType(MIME)类型：text/tab-separated-values
        /// </summary>
        [Description("text/tab-separated-values")]
        _tsv,
        /// <summary>
        /// 扩展名.ttf的文件，对应ContentType(MIME)类型：application/octet-stream
        /// </summary>
        [Description("application/octet-stream")]
        _ttf,
        /// <summary>
        /// 扩展名.ttz的文件，对应ContentType(MIME)类型：application/t-time
        /// </summary>
        [Description("application/t-time")]
        _ttz,
        /// <summary>
        /// 扩展名.txt的文件，对应ContentType(MIME)类型：text/plain
        /// </summary>
        [Description("text/plain")]
        _txt,
        /// <summary>
        /// 扩展名.uls的文件，对应ContentType(MIME)类型：text/iuls
        /// </summary>
        [Description("text/iuls")]
        _uls,
        /// <summary>
        /// 扩展名.ult的文件，对应ContentType(MIME)类型：audio/x-mod
        /// </summary>
        [Description("audio/x-mod")]
        _ult,
        /// <summary>
        /// 扩展名.ustar的文件，对应ContentType(MIME)类型：application/x-ustar
        /// </summary>
        [Description("application/x-ustar")]
        _ustar,
        /// <summary>
        /// 扩展名.uu的文件，对应ContentType(MIME)类型：application/x-uuencode
        /// </summary>
        [Description("application/x-uuencode")]
        _uu,
        /// <summary>
        /// 扩展名.uue的文件，对应ContentType(MIME)类型：application/x-uuencode
        /// </summary>
        [Description("application/x-uuencode")]
        _uue,
        /// <summary>
        /// 扩展名.vcd的文件，对应ContentType(MIME)类型：application/x-cdlink
        /// </summary>
        [Description("application/x-cdlink")]
        _vcd,
        /// <summary>
        /// 扩展名.vcf的文件，对应ContentType(MIME)类型：text/x-vcard
        /// </summary>
        [Description("text/x-vcard")]
        _vcf,
        /// <summary>
        /// 扩展名.vdo的文件，对应ContentType(MIME)类型：video/vdo
        /// </summary>
        [Description("video/vdo")]
        _vdo,
        /// <summary>
        /// 扩展名.vib的文件，对应ContentType(MIME)类型：audio/vib
        /// </summary>
        [Description("audio/vib")]
        _vib,
        /// <summary>
        /// 扩展名.viv的文件，对应ContentType(MIME)类型：video/vivo
        /// </summary>
        [Description("video/vivo")]
        _viv,
        /// <summary>
        /// 扩展名.vivo的文件，对应ContentType(MIME)类型：video/vivo
        /// </summary>
        [Description("video/vivo")]
        _vivo,
        /// <summary>
        /// 扩展名.vmd的文件，对应ContentType(MIME)类型：application/vocaltec-media-desc
        /// </summary>
        [Description("application/vocaltec-media-desc")]
        _vmd,
        /// <summary>
        /// 扩展名.vmf的文件，对应ContentType(MIME)类型：application/vocaltec-media-file
        /// </summary>
        [Description("application/vocaltec-media-file")]
        _vmf,
        /// <summary>
        /// 扩展名.vmi的文件，对应ContentType(MIME)类型：application/x-dreamcast-vms-info
        /// </summary>
        [Description("application/x-dreamcast-vms-info")]
        _vmi,
        /// <summary>
        /// 扩展名.vms的文件，对应ContentType(MIME)类型：application/x-dreamcast-vms
        /// </summary>
        [Description("application/x-dreamcast-vms")]
        _vms,
        /// <summary>
        /// 扩展名.vox的文件，对应ContentType(MIME)类型：audio/voxware
        /// </summary>
        [Description("audio/voxware")]
        _vox,
        /// <summary>
        /// 扩展名.vqe的文件，对应ContentType(MIME)类型：audio/x-twinvq-plugin
        /// </summary>
        [Description("audio/x-twinvq-plugin")]
        _vqe,
        /// <summary>
        /// 扩展名.vqf的文件，对应ContentType(MIME)类型：audio/x-twinvq
        /// </summary>
        [Description("audio/x-twinvq")]
        _vqf,
        /// <summary>
        /// 扩展名.vql的文件，对应ContentType(MIME)类型：audio/x-twinvq
        /// </summary>
        [Description("audio/x-twinvq")]
        _vql,
        /// <summary>
        /// 扩展名.vre的文件，对应ContentType(MIME)类型：x-world/x-vream
        /// </summary>
        [Description("x-world/x-vream")]
        _vre,
        /// <summary>
        /// 扩展名.vrml的文件，对应ContentType(MIME)类型：x-world/x-vrml
        /// </summary>
        [Description("x-world/x-vrml")]
        _vrml,
        /// <summary>
        /// 扩展名.vrt的文件，对应ContentType(MIME)类型：x-world/x-vrt
        /// </summary>
        [Description("x-world/x-vrt")]
        _vrt,
        /// <summary>
        /// 扩展名.vrw的文件，对应ContentType(MIME)类型：x-world/x-vream
        /// </summary>
        [Description("x-world/x-vream")]
        _vrw,
        /// <summary>
        /// 扩展名.vts的文件，对应ContentType(MIME)类型：workbook/formulaone
        /// </summary>
        [Description("workbook/formulaone")]
        _vts,
        /// <summary>
        /// 扩展名.wav的文件，对应ContentType(MIME)类型：audio/x-wav
        /// </summary>
        [Description("audio/x-wav")]
        _wav,
        /// <summary>
        /// 扩展名.wax的文件，对应ContentType(MIME)类型：audio/x-ms-wax
        /// </summary>
        [Description("audio/x-ms-wax")]
        _wax,
        /// <summary>
        /// 扩展名.wbmp的文件，对应ContentType(MIME)类型：image/vnd.wap.wbmp
        /// </summary>
        [Description("image/vnd.wap.wbmp")]
        _wbmp,
        /// <summary>
        /// 扩展名.wcm的文件，对应ContentType(MIME)类型：application/vnd.ms-works
        /// </summary>
        [Description("application/vnd.ms-works")]
        _wcm,
        /// <summary>
        /// 扩展名.wdb的文件，对应ContentType(MIME)类型：application/vnd.ms-works
        /// </summary>
        [Description("application/vnd.ms-works")]
        _wdb,
        /// <summary>
        /// 扩展名.web的文件，对应ContentType(MIME)类型：application/vnd.xara
        /// </summary>
        [Description("application/vnd.xara")]
        _web,
        /// <summary>
        /// 扩展名.wi的文件，对应ContentType(MIME)类型：image/wavelet
        /// </summary>
        [Description("image/wavelet")]
        _wi,
        /// <summary>
        /// 扩展名.wis的文件，对应ContentType(MIME)类型：application/x-InstallShield
        /// </summary>
        [Description("application/x-InstallShield")]
        _wis,
        /// <summary>
        /// 扩展名.wks的文件，对应ContentType(MIME)类型：application/vnd.ms-works
        /// </summary>
        [Description("application/vnd.ms-works")]
        _wks,
        /// <summary>
        /// 扩展名.wm的文件，对应ContentType(MIME)类型：video/x-ms-wm
        /// </summary>
        [Description("video/x-ms-wm")]
        _wm,
        /// <summary>
        /// 扩展名.wma的文件，对应ContentType(MIME)类型：audio/x-ms-wma
        /// </summary>
        [Description("audio/x-ms-wma")]
        _wma,
        /// <summary>
        /// 扩展名.wmd的文件，对应ContentType(MIME)类型：application/x-ms-wmd
        /// </summary>
        [Description("application/x-ms-wmd")]
        _wmd,
        /// <summary>
        /// 扩展名.wmf的文件，对应ContentType(MIME)类型：application/x-msmetafile
        /// </summary>
        [Description("application/x-msmetafile")]
        _wmf,
        /// <summary>
        /// 扩展名.wml的文件，对应ContentType(MIME)类型：text/vnd.wap.wml
        /// </summary>
        [Description("text/vnd.wap.wml")]
        _wml,
        /// <summary>
        /// 扩展名.wmlc的文件，对应ContentType(MIME)类型：application/vnd.wap.wmlc
        /// </summary>
        [Description("application/vnd.wap.wmlc")]
        _wmlc,
        /// <summary>
        /// 扩展名.wmls的文件，对应ContentType(MIME)类型：text/vnd.wap.wmlscript
        /// </summary>
        [Description("text/vnd.wap.wmlscript")]
        _wmls,
        /// <summary>
        /// 扩展名.wmlsc的文件，对应ContentType(MIME)类型：application/vnd.wap.wmlscriptc
        /// </summary>
        [Description("application/vnd.wap.wmlscriptc")]
        _wmlsc,
        /// <summary>
        /// 扩展名.wmlscript的文件，对应ContentType(MIME)类型：text/vnd.wap.wmlscript
        /// </summary>
        [Description("text/vnd.wap.wmlscript")]
        _wmlscript,
        /// <summary>
        /// 扩展名.wmv的文件，对应ContentType(MIME)类型：audio/x-ms-wmv
        /// </summary>
        [Description("audio/x-ms-wmv")]
        _wmv,
        /// <summary>
        /// 扩展名.wmx的文件，对应ContentType(MIME)类型：video/x-ms-wmx
        /// </summary>
        [Description("video/x-ms-wmx")]
        _wmx,
        /// <summary>
        /// 扩展名.wmz的文件，对应ContentType(MIME)类型：application/x-ms-wmz
        /// </summary>
        [Description("application/x-ms-wmz")]
        _wmz,
        /// <summary>
        /// 扩展名.wpng的文件，对应ContentType(MIME)类型：image/x-up-wpng
        /// </summary>
        [Description("image/x-up-wpng")]
        _wpng,
        /// <summary>
        /// 扩展名.wps的文件，对应ContentType(MIME)类型：application/vnd.ms-works
        /// </summary>
        [Description("application/vnd.ms-works")]
        _wps,
        /// <summary>
        /// 扩展名.wpt的文件，对应ContentType(MIME)类型：x-lml/x-gps
        /// </summary>
        [Description("x-lml/x-gps")]
        _wpt,
        /// <summary>
        /// 扩展名.wri的文件，对应ContentType(MIME)类型：application/x-mswrite
        /// </summary>
        [Description("application/x-mswrite")]
        _wri,
        /// <summary>
        /// 扩展名.wrl的文件，对应ContentType(MIME)类型：x-world/x-vrml
        /// </summary>
        [Description("x-world/x-vrml")]
        _wrl,
        /// <summary>
        /// 扩展名.wrz的文件，对应ContentType(MIME)类型：x-world/x-vrml
        /// </summary>
        [Description("x-world/x-vrml")]
        _wrz,
        /// <summary>
        /// 扩展名.ws的文件，对应ContentType(MIME)类型：text/vnd.wap.wmlscript
        /// </summary>
        [Description("text/vnd.wap.wmlscript")]
        _ws,
        /// <summary>
        /// 扩展名.wsc的文件，对应ContentType(MIME)类型：application/vnd.wap.wmlscriptc
        /// </summary>
        [Description("application/vnd.wap.wmlscriptc")]
        _wsc,
        /// <summary>
        /// 扩展名.wv的文件，对应ContentType(MIME)类型：video/wavelet
        /// </summary>
        [Description("video/wavelet")]
        _wv,
        /// <summary>
        /// 扩展名.wvx的文件，对应ContentType(MIME)类型：video/x-ms-wvx
        /// </summary>
        [Description("video/x-ms-wvx")]
        _wvx,
        /// <summary>
        /// 扩展名.wxl的文件，对应ContentType(MIME)类型：application/x-wxl
        /// </summary>
        [Description("application/x-wxl")]
        _wxl,
        /// <summary>
        /// 扩展名.xaf的文件，对应ContentType(MIME)类型：x-world/x-vrml
        /// </summary>
        [Description("x-world/x-vrml")]
        _xaf,
        /// <summary>
        /// 扩展名.xar的文件，对应ContentType(MIME)类型：application/vnd.xara
        /// </summary>
        [Description("application/vnd.xara")]
        _xar,
        /// <summary>
        /// 扩展名.xbm的文件，对应ContentType(MIME)类型：image/x-xbitmap
        /// </summary>
        [Description("image/x-xbitmap")]
        _xbm,
        /// <summary>
        /// 扩展名.xdm的文件，对应ContentType(MIME)类型：application/x-xdma
        /// </summary>
        [Description("application/x-xdma")]
        _xdm,
        /// <summary>
        /// 扩展名.xdma的文件，对应ContentType(MIME)类型：application/x-xdma
        /// </summary>
        [Description("application/x-xdma")]
        _xdma,
        /// <summary>
        /// 扩展名.xdw的文件，对应ContentType(MIME)类型：application/vnd.fujixerox.docuworks
        /// </summary>
        [Description("application/vnd.fujixerox.docuworks")]
        _xdw,
        /// <summary>
        /// 扩展名.xht的文件，对应ContentType(MIME)类型：application/xhtml+xml
        /// </summary>
        [Description("application/xhtml+xml")]
        _xht,
        /// <summary>
        /// 扩展名.xhtm的文件，对应ContentType(MIME)类型：application/xhtml+xml
        /// </summary>
        [Description("application/xhtml+xml")]
        _xhtm,
        /// <summary>
        /// 扩展名.xhtml的文件，对应ContentType(MIME)类型：application/xhtml+xml
        /// </summary>
        [Description("application/xhtml+xml")]
        _xhtml,
        /// <summary>
        /// 扩展名.xla的文件，对应ContentType(MIME)类型：application/vnd.ms-excel
        /// </summary>
        [Description("application/vnd.ms-excel")]
        _xla,
        /// <summary>
        /// 扩展名.xlc的文件，对应ContentType(MIME)类型：application/vnd.ms-excel
        /// </summary>
        [Description("application/vnd.ms-excel")]
        _xlc,
        /// <summary>
        /// 扩展名.xll的文件，对应ContentType(MIME)类型：application/x-excel
        /// </summary>
        [Description("application/x-excel")]
        _xll,
        /// <summary>
        /// 扩展名.xlm的文件，对应ContentType(MIME)类型：application/vnd.ms-excel
        /// </summary>
        [Description("application/vnd.ms-excel")]
        _xlm,
        /// <summary>
        /// 扩展名.xls的文件，对应ContentType(MIME)类型：application/vnd.ms-excel
        /// </summary>
        [Description("application/vnd.ms-excel")]
        _xls,
        /// <summary>
        /// 扩展名.xlsx的文件，对应ContentType(MIME)类型：application/vnd.openxmlformats-officedocument.spreadsheetml.sheet
        /// </summary>
        [Description("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        _xlsx,
        /// <summary>
        /// 扩展名.xlt的文件，对应ContentType(MIME)类型：application/vnd.ms-excel
        /// </summary>
        [Description("application/vnd.ms-excel")]
        _xlt,
        /// <summary>
        /// 扩展名.xlw的文件，对应ContentType(MIME)类型：application/vnd.ms-excel
        /// </summary>
        [Description("application/vnd.ms-excel")]
        _xlw,
        /// <summary>
        /// 扩展名.xm的文件，对应ContentType(MIME)类型：audio/x-mod
        /// </summary>
        [Description("audio/x-mod")]
        _xm,
        /// <summary>
        /// 扩展名.xml的文件，对应ContentType(MIME)类型：text/plain
        /// </summary>
        [Description("text/plain")]
        _xml,
        /// <summary>
        /// 扩展名.xmz的文件，对应ContentType(MIME)类型：audio/x-mod
        /// </summary>
        [Description("audio/x-mod")]
        _xmz,
        /// <summary>
        /// 扩展名.xof的文件，对应ContentType(MIME)类型：x-world/x-vrml
        /// </summary>
        [Description("x-world/x-vrml")]
        _xof,
        /// <summary>
        /// 扩展名.xpi的文件，对应ContentType(MIME)类型：application/x-xpinstall
        /// </summary>
        [Description("application/x-xpinstall")]
        _xpi,
        /// <summary>
        /// 扩展名.xpm的文件，对应ContentType(MIME)类型：image/x-xpixmap
        /// </summary>
        [Description("image/x-xpixmap")]
        _xpm,
        /// <summary>
        /// 扩展名.xsit的文件，对应ContentType(MIME)类型：text/xml
        /// </summary>
        [Description("text/xml")]
        _xsit,
        /// <summary>
        /// 扩展名.xsl的文件，对应ContentType(MIME)类型：text/xml
        /// </summary>
        [Description("text/xml")]
        _xsl,
        /// <summary>
        /// 扩展名.xul的文件，对应ContentType(MIME)类型：text/xul
        /// </summary>
        [Description("text/xul")]
        _xul,
        /// <summary>
        /// 扩展名.xwd的文件，对应ContentType(MIME)类型：image/x-xwindowdump
        /// </summary>
        [Description("image/x-xwindowdump")]
        _xwd,
        /// <summary>
        /// 扩展名.xyz的文件，对应ContentType(MIME)类型：chemical/x-pdb
        /// </summary>
        [Description("chemical/x-pdb")]
        _xyz,
        /// <summary>
        /// 扩展名.yz1的文件，对应ContentType(MIME)类型：application/x-yz1
        /// </summary>
        [Description("application/x-yz1")]
        _yz1,
        /// <summary>
        /// 扩展名.z的文件，对应ContentType(MIME)类型：application/x-compress
        /// </summary>
        [Description("application/x-compress")]
        _z,
        /// <summary>
        /// 扩展名.zac的文件，对应ContentType(MIME)类型：application/x-zaurus-zac
        /// </summary>
        [Description("application/x-zaurus-zac")]
        _zac,
        /// <summary>
        /// 扩展名.zip的文件，对应ContentType(MIME)类型：application/zip
        /// </summary>
        [Description("application/zip")]
        _zip,
        /// <summary>
        /// 扩展名.json的文件，对应ContentType(MIME)类型：application/json
        /// </summary>
        [Description("application/json")]
        _json

    }
}
