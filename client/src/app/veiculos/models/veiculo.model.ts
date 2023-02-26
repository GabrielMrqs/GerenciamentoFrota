import { TipoVeiculo } from "./tipo-veiculo";

export interface Veiculo {
    chassi: string;
    tipoVeiculo: TipoVeiculo
    numeroPassageiros: number;
    cor: string;
}
