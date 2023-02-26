import { Component, OnInit } from '@angular/core';
import { NonNullableFormBuilder, Validators } from '@angular/forms';
import { VeiculoService } from '../../services/veiculo.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Location } from '@angular/common';
import { TipoVeiculo } from '../../models/tipo-veiculo';
import { ActivatedRoute } from '@angular/router';
import { Veiculo } from '../../models/veiculo.model';

@Component({
  selector: 'app-adicionar',
  templateUrl: './adicionar.component.html',
  styleUrls: ['./adicionar.component.scss']
})
export class AdicionarComponent implements OnInit {

  private veiculo: Veiculo;
  private ehEdicao: boolean;

  form = this.formBuilder.group({
    chassi: ['', [
      Validators.required,
      Validators.minLength(17),
      Validators.maxLength(17)]
    ],
    tipoVeiculo: [TipoVeiculo.Ônibus, Validators.required],
    numeroPassageiros: [42, Validators.required],
    cor: ['', Validators.required],
  });

  constructor(
    private formBuilder: NonNullableFormBuilder,
    private service: VeiculoService,
    private snackBar: MatSnackBar,
    private location: Location,
    private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.veiculo = this.route.snapshot.data['veiculo'];
    this.form.setValue(this.veiculo);
    this.verificarSeEdicao(this.veiculo);
  }

  onSubmit() {
    if (this.form.invalid) {
      this.snackBar.open('Erro ao inserir veículo, confira os campos do formulário.', 'x', { duration: 5000 });
      return;
    }
    
    if (this.ehEdicao) {
      this.patchValores();
      this.service.atualizarVeiculo(this.form.value)
        .subscribe(() => this.onSuccess());
    } else {
      this.service.inserirVeiculo(this.form.value)
        .subscribe(() => this.onSuccess());
    }
  }

  onCancel() {
    this.location.back();
  }

  getErrorMessage(fieldName: string) {
    const field = this.form.get(fieldName);

    if (field?.hasError('minlength') || field?.hasError('maxlength')) {
      return 'O chassi precisa ter 17 caracteres';
    }

    if (field?.hasError('required')) {
      return 'Campo obrigatório';
    }

    return 'Campo inválido'
  }

  tipoVeiculoPassageiros(tipoVeiculo: TipoVeiculo) {
    var numeroPassageiros = tipoVeiculo === TipoVeiculo.Caminhão ? 2 : 42;

    this.form.patchValue({
      numeroPassageiros: numeroPassageiros
    })
  }

  private onSuccess() {
    this.snackBar.open('Veículo inserido com sucesso!!!', 'x', { duration: 5000 });
    this.location.back();
  }

  private verificarSeEdicao(veiculo: Veiculo) {
    this.ehEdicao = veiculo.chassi !== '' ? true : false;

    if (this.ehEdicao) {
      this.form.controls.tipoVeiculo.disable();
      this.form.controls.chassi.disable();
      this.form.controls.numeroPassageiros.disable();
    }

  }

  private patchValores() {
    this.form.value.tipoVeiculo = this.veiculo.tipoVeiculo;
    this.form.value.chassi = this.veiculo.chassi;
    this.form.value.numeroPassageiros = this.veiculo.numeroPassageiros;
  }
}
