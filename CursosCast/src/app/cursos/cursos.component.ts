import { ToastrService } from 'ngx-toastr';
import { Curso, Categoria } from './../Utils/interfaces';
import { Component, OnInit } from '@angular/core';
import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';
import { CursoService } from '../services/curso.service';
import { FormBuilder } from '@angular/forms';
import { findIndex } from 'rxjs/operators';

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
  index: number;
  teste: string;
  filterTerm: string;
  cursoTeste: Curso;

  closeResult: string = '';

  cursoForm = this.fb.group({
    descricao: [''],
    dataInicio: [''],
    dataTermino: [''],
    quantidadeAlunos: [''],
    categoriaId: ['']
  });

  cursoEditForm = this.fb.group({
    id:[this.cursos.map(x => x.id)],
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

  public async getById(curso: Curso) {
    this._service.getCursoById(curso.id.toString()).subscribe(dados => this.cursos = dados)
    this.teste = curso.descricao
    console.log(this.teste)
  }

  public async save() {
    await this._service.addCurso(this.cursoForm.value).subscribe(res => {
      this._toastrService.success("Registro inserido com sucesso")
      console.log(res);
    });
  }

  public async update() {
    await this._service.updateCurso(this.cursoForm.value).subscribe(res => {
      this._toastrService.success("Registro atualizado com sucesso")
      console.log(res);
    });
  }

  public async delete(curso: Curso){
    await this._service.deleteCurso(curso.id.toString()).subscribe(res =>{
      this._toastrService.success("Registro deletado com sucesso")
      console.log(res);
    });
  }
}
