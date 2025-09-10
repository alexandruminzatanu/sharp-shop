import { RouterModule } from '@angular/router';
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductsComponent } from './products/products.component';



@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterModule, ProductsComponent],
  template: `
  <nav class="navbar navbar-expand navbar-dark bg-dark mb-3">
    <a class="navbar-brand ms-2" routerLink="/">Sharp Shop</a>
    <ul class="navbar-nav ms-3">
      <li class="nav-item"><a class="nav-link" routerLink="/products" routerLinkActive="active">Products</a></li>
    </ul>
  </nav>
  <div class="container">
    <router-outlet></router-outlet>
  </div>
  `
})
export class AppComponent { }
