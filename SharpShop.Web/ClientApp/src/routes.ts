import { ProductsComponent } from './app/products/products.component';
import { Routes } from '@angular/router';
export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'products' },
  { path: 'products', component: ProductsComponent }
];