import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CornService {
  private apiUrl = 'https://localhost:44387/api/Corn';

  constructor(private http: HttpClient) {}

  buyCorn(clientId: string) {
    return this.http.post(this.apiUrl + "/buyCorn/" + clientId, null);
  }
}