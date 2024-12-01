export interface IFormaContribucionResponse {
    id: string;
    fechaContribucion: string;
    tipo: 'AdministracionHeladera' | 'DistribucionViandas' | 'DonacionMonetaria' | 'DonacionVianda' | 'OfertaPremio' | 'RegistroPersonaVulnerable';
}