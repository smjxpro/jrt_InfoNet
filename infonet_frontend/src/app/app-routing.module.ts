import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {HomeComponent} from "./pages/home/home.component";
import {AddComponent} from "./pages/add/add.component";
import {EditComponent} from "./pages/edit/edit.component";
import {DetailsComponent} from "./pages/details/details.component";
import {NotFoundComponent} from "./pages/not-found/not-found.component";


const routes: Routes = [
  {
    path: '', redirectTo: 'home', pathMatch: 'full'
  },
  {
    path: 'home', component: HomeComponent,
  },
  {
    path: "details/:id", component: DetailsComponent,
  },
  {
    path: "add", component: AddComponent,
  },
  {
    path: "edit/:id", component: EditComponent,
  },
  {
    path:"404", component: NotFoundComponent,
  },
  {
    path: "**", redirectTo: "404"
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
