import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, delay, first, of, retry, take, throwError } from 'rxjs';
import { Veiculo } from '../models/veiculo.model';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from 'src/app/shared/components/error-dialog/error-dialog.component';

@Injectable({
  providedIn: 'root'
})
export class VeiculoService {

  private url = "https://localhost:60783"

  constructor(private http: HttpClient,
    public dialog: MatDialog) { }

  obterTodosVeiculos(): Observable<Veiculo[]> {
    return this.http.get<Veiculo[]>(`${this.url}/VisualizarTodosVeiculos`)
      .pipe(
        first(),
        // delay(5000),
        retry(1),
        catchError(error => {
          this.onError(error.error)
          return of([]);
        })
      );
  }

  obterVeiculosPeloChassi(chassi: string): Observable<Veiculo> {
    return this.http.get<Veiculo>(`${this.url}/VisualizarVeiculoPorChassi`, { params: { chassi: chassi } })
      .pipe(
        first(),
        // delay(5000),
        retry(1),
        catchError(error => {
          this.onError(error.error)
          return of();
        })
      );
  }

  inserirVeiculo(veiculo: Partial<Veiculo>): Observable<Veiculo> {
    return this.http.post<Veiculo>(`${this.url}/InserirVeiculo`, { Veiculo: veiculo })
      .pipe(
        first(),
        // delay(5000),
        retry(1),
        catchError(error => {
          this.onError(error.error)
          return of();
        })
      );
  }

  atualizarVeiculo(veiculo: Partial<Veiculo>) {
    return this.http.put(`${this.url}/EditarVeiculo`, { Veiculo: veiculo })
      .pipe(
        first(),
        // delay(5000),
        retry(1),
        catchError(error => {
          this.onError(error.error)
          return of();
        })
      );
  }

  excluirVeiculo(chassi: string) {
    return this.http.delete(`${this.url}/ExcluirVeiculo`, { params: { chassi: chassi } })
      .pipe(
        first(),
        // delay(5000),
        retry(1),
        catchError(error => {
          this.onError(error.error)
          return of();
        })
      );
  }

  private onError(errorMsg: string) {
    this.dialog.open(ErrorDialogComponent, {
      data: errorMsg
    })
  }
}
