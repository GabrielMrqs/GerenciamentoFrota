import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AdicionarComponent } from "./components/adicionar/adicionar.component";
import { VeiculoComponent } from "./containers/veiculo/veiculo.component";
import { VeiculoResolver } from './guards/veiculo.resolver';

const routes: Routes = [
    { path: '', component: VeiculoComponent },
    { path: 'new', component: AdicionarComponent, resolve: { veiculo: VeiculoResolver }},
    { path: 'edit/:chassi', component: AdicionarComponent, resolve: { veiculo: VeiculoResolver } },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class VeiculosRoutingModule { }