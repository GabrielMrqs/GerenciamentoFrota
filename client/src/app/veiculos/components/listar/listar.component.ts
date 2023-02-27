import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Veiculo } from '../../models/veiculo.model';
import { TipoVeiculo } from '../../models/tipo-veiculo';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-listar',
  templateUrl: './listar.component.html',
  styleUrls: ['./listar.component.scss']
})
export class ListarComponent implements OnInit {


  @Input() veiculos: Veiculo[];
  @Output() add = new EventEmitter(false);
  @Output() edit = new EventEmitter(false);
  @Output() delete = new EventEmitter(false);
  dataSource: MatTableDataSource<Veiculo>;

  
  displayedColumns = ['chassi', 'tipoVeiculo', 'cor', 'numeroPassageiros', 'actions'];
  ngOnInit(): void {
    this.dataSource =  new MatTableDataSource(this.veiculos);
    this.dataSource.filterPredicate = (data: Veiculo, filter: string) => {
      return data.chassi == filter;
     };
  }

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

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
}
