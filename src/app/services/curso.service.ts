import { Categoria } from './../Utils/interfaces';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Curso } from '../Utils/interfaces';
import { tap} from  'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class CursoService {
  readonly base_url = "https://localhost:5001"
  constructor(private httpClient: HttpClient) {

  }

  public getCurso(): Observable<Curso[]> {
    return this.httpClient.get<Curso[]>(`${this.base_url}/api/Curso/`).pipe(tap(console.log));
  }

  public getCategoria(): Observable<Categoria[]> {
    return this.httpClient.get<Categoria[]>(`${this.base_url}/api/Curso/categoria`).pipe(tap(console.log));
  }

  public addCurso(curso: Curso): Observable<Curso> {
    return this.httpClient.post<Curso>(`${this.base_url}/api/Curso/add-curso`,curso);
  }
}
