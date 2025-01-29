import { CommonModule  } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CornService } from './services/corn.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  clientId: string = ''; 
  message: string | null = null; 
  success: boolean = false;

  constructor(private cornService: CornService) {}


  buyCorn(){
    if (!this.clientId.trim()) {
      this.message = 'Please enter a valid Client ID.';
      this.success = false;
      return;
    }

    this.cornService.buyCorn(this.clientId).subscribe(
      (response: any) => {
        this.message = "Corn Successfully Bought!";
        this.success = true;
      },
      (error) => {
        if (error.status === 429) {
          this.message = 'Too Many Requests! Try again later.';
        } else {
          this.message = 'An error occurred.';
        }
        this.success = false;
      }
    );
  }
}
