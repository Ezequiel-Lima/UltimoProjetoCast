import { ToastrService } from 'ngx-toastr';
import { Curso, Categoria } from './../Utils/interfaces';
import { Component, OnInit } from '@angular/core';
import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';
import { CursoService } from '../services/curso.service';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-cursos',
  templateUrl: './cursos.component.html',
  styleUrls: ['./cursos.component.css']
})

export class CursosComponent implements OnInit {
  title = 'appBootstrap';
  cursos: Curso[] = [];
  categorias: Categoria[] = [];
  categoriaSelecionada: Categoria;

  closeResult: string = '';

  cursoForm = this.fb.group({
    descricao: [''],
    dataInicio: [''],
    dataTermino: [''],
    quantidadeAlunos: [''],
    categoriaId: ['']
  });

  constructor(private modalService: NgbModal, private _service: CursoService, private fb: FormBuilder, private _toastrService: ToastrService) {
  }

  ngOnInit(): void {
    this._service.getCurso().subscribe(dados => this.cursos = dados);
    this._service.getCategoria().subscribe(data => this.categorias = data);
  }

  open(content:any) {
    this.modalService.open(content, {ariaLabelledBy: 'modal-basic-title'}).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return  `with: ${reason}`;
    }
  }

  public async save() {
    await this._service.addCurso(this.cursoForm.value).subscribe(res => {
      this._toastrService.success("Registro inserido com suceeso")
      console.log(res);
    });
  }
}
