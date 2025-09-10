import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Product, ProductsService } from './products.service';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [CommonModule],
  template: `
  <h1>Products</h1>
  <p>This list is fetched from the backend API.</p>
  <button class="btn btn-primary mb-3" (click)="reload()">Reload</button>
  <table *ngIf="products?.length; else loading" class="table table-striped">
    <thead>
      <tr><th>Name</th><th>Description</th></tr>
    </thead>
    <tbody>
      <tr *ngFor="let p of products">
        <td>{{p.name}}</td>
        <td>{{p.description}}</td>
      </tr>
    </tbody>
  </table>
  <ng-template #loading><em>Loading...</em></ng-template>
  `
})
export class ProductsComponent implements OnInit {
  products: Product[] = [];
  loading = false;
  constructor(private svc: ProductsService) {}
  ngOnInit() { this.reload(); }
  reload() {
    this.loading = true;
    this.svc.getProducts().subscribe(list => { this.products = list; this.loading = false; });
  }
}
