import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Veiculo } from '../../models/veiculo.model';
import { Router, ActivatedRoute } from '@angular/router';
import { VeiculoService } from '../../services/veiculo.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';

@Component({
  selector: 'app-veiculo',
  templateUrl: './veiculo.component.html',
  styleUrls: ['./veiculo.component.scss']
})
export class VeiculoComponent implements OnInit {
  veiculos$: Observable<Veiculo[]>;
  displayedColumns = ['chassi', 'tipoVeiculo', 'cor', 'numeroPassageiros', 'actions'];

  constructor(
    private service: VeiculoService,
    private router: Router,
    private route: ActivatedRoute,
    private snackBar: MatSnackBar,
    private dialog: MatDialog) {
  }

  ngOnInit(): void {
    this.refresh();
  }

  onAdd() {
    this.router.navigate(['new'], { relativeTo: this.route });
  }

  onEdit(veiculo: Veiculo) {
    this.router.navigate(['edit', veiculo.chassi], { relativeTo: this.route });
  }

  onDelete(veiculo: Veiculo) {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      data: 'Tem certeza que deseja remover esse veículo?',
    });

    dialogRef.afterClosed().subscribe((result: boolean) => {
      if (result) {
        this.service.excluirVeiculo(veiculo.chassi)
          .subscribe(() => {
            this.refresh();
            this.onSuccess();
          });
      }
    });
  }

  private refresh() {
    this.veiculos$ = this.service.obterTodosVeiculos();
  }

  private onSuccess() {
    this.snackBar.open('Veículo removido com sucesso!!!', 'x', {
      duration: 5000,
      verticalPosition: 'top',
      horizontalPosition: 'center'
    });
  }
}
