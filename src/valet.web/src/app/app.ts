import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Introduction } from "./modules/introduction/introduction";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Introduction],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('valet.web');
}
