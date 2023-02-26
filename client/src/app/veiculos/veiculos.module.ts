import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VeiculosRoutingModule } from './veiculos-routing.module';

import { ListarComponent } from './components/listar/listar.component';
import { AppMaterialModule } from '../shared/app-material/app-material.module';
import { SharedModule } from '../shared/shared.module';
import { AdicionarComponent } from './components/adicionar/adicionar.component';
import { ReactiveFormsModule } from '@angular/forms';
import { VeiculoComponent } from './containers/veiculo/veiculo.component';


@NgModule({
  declarations: [
    ListarComponent,
    AdicionarComponent,
    VeiculoComponent
  ],
  imports: [
    CommonModule,
    VeiculosRoutingModule,
    AppMaterialModule,
    SharedModule,
    ReactiveFormsModule,
  ]
})
export class VeiculosModule { }
