import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { FormsModule } from "@angular/forms";
import { NgChartsModule } from 'ng2-charts';
import { SummaryCardsComponent } from './pages/dashboard/components/summary-cards/summary-cards/summary-cards.component';
import { TrendChartComponent } from './pages/dashboard/components/trend-chart/trend-chart/trend-chart.component';
import { CategoryChartComponent } from './pages/dashboard/components/category-chart/category-chart/category-chart.component';




@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    SummaryCardsComponent,
    TrendChartComponent,
    CategoryChartComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    NgChartsModule
],
  providers: [
   { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
