import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { VeiculoService } from '../services/veiculo.service';
import { Veiculo } from '../models/veiculo.model';
import { TipoVeiculo } from '../models/tipo-veiculo';

@Injectable({
  providedIn: 'root'
})
export class VeiculoResolver implements Resolve<Veiculo> {

  constructor(private service: VeiculoService) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Veiculo> {
    if (route.params && route.params['chassi']) {
      return this.service.obterVeiculosPeloChassi(route.params['chassi']);
    }

    return of({ cor: '', chassi: '', tipoVeiculo: TipoVeiculo.Ônibus, numeroPassageiros: 42 });
  }
}
