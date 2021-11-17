export interface Curso {
  id: number;
  descricao : string;
  dataInicio: Date;
  dataTermino: Date;
  quantidadeAlunos: number;
  categoriaId: number;
}

export interface Categoria {
  id: number;
  nome: string;
}
