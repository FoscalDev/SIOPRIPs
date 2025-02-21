using Newtonsoft.Json;

namespace SIOP.Model.DTO.DockerRips
{
    public class CargueFevRipsDTO
    {
        public RipsDTO? rips { get; set; }
        public string? xmlFevFile { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? einri { get; set; }
    }

    public class RipsDTO
    {
        public string? numDocumentoIdObligado { get; set; }
        public string? numFactura { get; set; }
        public string? tipoNota { get; set; }
        public string? numNota { get; set; }
        public List<UsuarioDTO>? usuarios { get; set; }
    }

    public class UsuarioDTO
    {
        public string? tipoDocumentoIdentificacion { get; set; }
        public string? numDocumentoIdentificacion { get; set; }
        public string? tipoUsuario { get; set; }
        public string? fechaNacimiento { get; set; }
        public string? codSexo { get; set; }
        public string? codPaisResidencia { get; set; }
        public string? codPaisOrigen { get; set; }
        public string? codMunicipioResidencia { get; set; }
        public string? codZonaTerritorialResidencia { get; set; }
        public string? incapacidad { get; set; }
        public int? consecutivo { get; set; }
        public ServiciosDTO? servicios { get; set; }
    }

    public class ServiciosDTO
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<ConsultaDTO>? consultas { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<ProcedimientoDTO>? procedimientos { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<UrgenciaDTO>? urgencias { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<HospitalizacionDTO>? hospitalizacion { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<RecienNacidoDTO>? recienNacidos { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<MedicamentoDTO>? medicamentos { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<OtroServicioDTO>? otrosServicios { get; set; }
    }

    public class ConsultaDTO
    {
        public string? codPrestador { get; set; }
        public string? fechaInicioAtencion { get; set; }
        public string? numAutorizacion { get; set; }
        public string? codConsulta { get; set; }
        public string? modalidadGrupoServicioTecSal { get; set; }
        public string? grupoServicios { get; set; }
        public int? codServicio { get; set; }
        public string? finalidadTecnologiaSalud { get; set; }
        public string? causaMotivoAtencion { get; set; }
        public string? codDiagnosticoPrincipal { get; set; }
        public string? codDiagnosticoRelacionado1 { get; set; }
        public string? codDiagnosticoRelacionado2 { get; set; }
        public string? codDiagnosticoRelacionado3 { get; set; }
        public string? tipoDiagnosticoPrincipal { get; set; }
        public string? tipoDocumentoIdentificacion { get; set; }
        public string? numDocumentoIdentificacion { get; set; }
        public long? vrServicio { get; set; }
        public string? conceptoRecaudo { get; set; }
        public long? valorPagoModerador { get; set; }
        public string? numFEVPagoModerador { get; set; }
        public int? consecutivo { get; set; }
    }

    public class ProcedimientoDTO
    {
        public string? codPrestador { get; set; }
        public string? fechaInicioAtencion { get; set; }
        public string? idMIPRES { get; set; }
        public string? numAutorizacion { get; set; }
        public string? codProcedimiento { get; set; }
        public string? viaIngresoServicioSalud { get; set; }
        public string? modalidadGrupoServicioTecSal { get; set; }
        public string? grupoServicios { get; set; }
        public int? codServicio { get; set; }
        public string? finalidadTecnologiaSalud { get; set; }
        public string? tipoDocumentoIdentificacion { get; set; }
        public string? numDocumentoIdentificacion { get; set; }
        public string? codDiagnosticoPrincipal { get; set; }
        public string? codDiagnosticoRelacionado { get; set; }
        public string? codComplicacion { get; set; }
        public long? vrServicio { get; set; }
        public string? conceptoRecaudo { get; set; }
        public long? valorPagoModerador { get; set; }
        public string? numFEVPagoModerador { get; set; }
        public int? consecutivo { get; set; }
    }

    public class UrgenciaDTO
    {
        public string? codPrestador { get; set; }
        public string? fechaInicioAtencion { get; set; }
        public string? causaMotivoAtencion { get; set; }
        public string? codDiagnosticoPrincipal { get; set; }
        public string? codDiagnosticoPrincipalE { get; set; }
        public string? codDiagnosticoRelacionadoE1 { get; set; }
        public string? codDiagnosticoRelacionadoE2 { get; set; }
        public string? codDiagnosticoRelacionadoE3 { get; set; }
        public string? condicionDestinoUsuarioEgreso { get; set; }
        public string? codDiagnosticoCausaMuerte { get; set; }
        public string? fechaEgreso { get; set; }
        public int? consecutivo { get; set; }
    }

    public class HospitalizacionDTO
    {
        public string? codPrestador { get; set; }
        public string? viaIngresoServicioSalud { get; set; }
        public string? fechaInicioAtencion { get; set; }
        public string? numAutorizacion { get; set; }
        public string? causaMotivoAtencion { get; set; }
        public string? codDiagnosticoPrincipal { get; set; }
        public string? codDiagnosticoPrincipalE { get; set; }
        public string? codDiagnosticoRelacionadoE1 { get; set; }
        public string? codDiagnosticoRelacionadoE2 { get; set; }
        public string? codDiagnosticoRelacionadoE3 { get; set; }
        public string? codComplicacion { get; set; }
        public string? condicionDestinoUsuarioEgreso { get; set; }
        public string? codDiagnosticoCausaMuerte { get; set; }
        public string? fechaEgreso { get; set; }
        public int? consecutivo { get; set; }
    }

    public class RecienNacidoDTO
    {
        public string? codPrestador { get; set; }
        public string? tipoDocumentoIdentificacion { get; set; }
        public string? numDocumentoIdentificacion { get; set; }
        public string? fechaNacimiento { get; set; }
        public int? edadGestacional { get; set; }
        public int? numConsultasCPrenatal { get; set; }
        public string? codSexoBiologico { get; set; }
        public int? peso { get; set; }
        public string? codDiagnosticoPrincipal { get; set; }
        public string? condicionDestinoUsuarioEgreso { get; set; }
        public string? codDiagnosticoCausaMuerte { get; set; }
        public string? fechaEgreso { get; set; }
        public int? consecutivo { get; set; }
    }

    public class MedicamentoDTO
    {
        public string? codPrestador { get; set; }
        public string? numAutorizacion { get; set; }
        public string? idMIPRES { get; set; }
        public string? fechaDispensAdmon { get; set; }
        public string? codDiagnosticoPrincipal { get; set; }
        public string? codDiagnosticoRelacionado { get; set; }
        public string? tipoMedicamento { get; set; }
        public string? codTecnologiaSalud { get; set; }
        public string? nomTecnologiaSalud { get; set; }
        public int? concentracionMedicamento { get; set; }
        public string? formaFarmaceutica { get; set; }
        public int? unidadMedida { get; set; }
        public int? unidadMinDispensa { get; set; }
        public long? cantidadMedicamento { get; set; }
        public int? diasTratamiento { get; set; }
        public string? tipoDocumentoIdentificacion { get; set; }
        public string? numDocumentoIdentificacion { get; set; }
        public long? vrUnitMedicamento { get; set; }
        public long? vrServicio { get; set; }
        public string? conceptoRecaudo { get; set; }
        public long? valorPagoModerador { get; set; }
        public string? numFEVPagoModerador { get; set; }
        public int? consecutivo { get; set; }
    }

    public class OtroServicioDTO
    {
        public string? codPrestador { get; set; }
        public string? numAutorizacion { get; set; }
        public string? idMIPRES { get; set; }
        public string? fechaSuministroTecnologia { get; set; }
        public string? tipoOS { get; set; }
        public string? codTecnologiaSalud { get; set; }
        public string? nomTecnologiaSalud { get; set; }
        public int? cantidadOS { get; set; }
        public string? tipoDocumentoIdentificacion { get; set; }
        public string? numDocumentoIdentificacion { get; set; }
        public long? vrUnitOS { get; set; }
        public long? vrServicio { get; set; }
        public string? conceptoRecaudo { get; set; }
        public long? valorPagoModerador { get; set; }
        public string? numFEVPagoModerador { get; set; }
        public int? consecutivo { get; set; }
    }

    public class RespuestaCargueFevRipsDTO
    {
        public bool resultState { get; set; }
        public int procesoId { get; set; }
        public string numFactura { get; set; }
        public string codigoUnicoValidacion { get; set; }
        public string codigoUnicoValidacionToShow { get; set; }
        public string fechaRadicacion { get; set; }
        public string rutaArchivos { get; set; }
        public List<ResultadoValidacion> resultadosValidacion { get; set; }

        public class ResultadoValidacion
        {
            public string clase { get; set; }
            public string codigo { get; set; }
            public string descripcion { get; set; }
            public string observaciones { get; set; }
            public string pathFuente { get; set; }
            public string fuente { get; set; }
        }
    }
}