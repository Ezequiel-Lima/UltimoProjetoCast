export interface ICurso {
  Id: number;
  Descricao : string;
  DataInicio: Date;
  DataTermino: Date;
  QuantidadeAlunos: number;
  CategoriaId: number;
  CategoriaDTO: ICategoriaDTO;
}

export interface ICategoriaDTO {
  Id: number;
  Nome: string;
}
