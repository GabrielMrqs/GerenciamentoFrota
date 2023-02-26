import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Veiculo } from '../../models/veiculo.model';
import { TipoVeiculo } from '../../models/tipo-veiculo';

@Component({
  selector: 'app-listar',
  templateUrl: './listar.component.html',
  styleUrls: ['./listar.component.scss']
})
export class ListarComponent {

  @Input() veiculos: Veiculo[];
  @Output() add = new EventEmitter(false);
  @Output() edit = new EventEmitter(false);
  @Output() delete = new EventEmitter(false);

  displayedColumns = ['chassi', 'tipoVeiculo', 'cor', 'numeroPassageiros', 'actions'];
  
  onAdd() {
    this.add.emit(true);
  }

  onEdit(veiculo: Veiculo) {
    this.edit.emit(veiculo);
  }

  onDelete(veiculo: Veiculo) {
    this.delete.emit(veiculo);
  }

  getTipoVeiculo(tipoVeiculo: TipoVeiculo): string {
    return TipoVeiculo[tipoVeiculo];
  }

}
