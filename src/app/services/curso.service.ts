import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ICurso } from '../Utils/interfaces';

@Injectable({
  providedIn: 'root'
})
export class CursoService {
  readonly base_url = "https://localhost:5001"
  constructor(private httpClient: HttpClient) {

  }

  public getCurso(): Observable<ICurso> {
    return this.httpClient.get<ICurso>(`${this.base_url}/api/Curso/`);
  }
}
