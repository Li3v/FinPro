import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { DashboardService } from '../../services/dashboard.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css',
})
export class DashboardComponent {
  summary: any;
  categories: any;
  trend: any;
  startDate: string = '';
  endDate: string = '';

  constructor(
    private authService: AuthService,
    private router: Router,
    private dashboardService: DashboardService,
  ) {}

  logOut() {
    this.authService.logOut();
    this.router.navigate(['/']);
  }

 /* ngOnInit(): void {
    this.loadDashboard();
  }*/

  loadDashboard() {

    const params = {
      startDate: this.startDate,
      endDate: this.endDate
    }

    this.dashboardService.getSummary(this.startDate, this.endDate).subscribe((data) => {
      this.summary = data;
    });

    this.dashboardService.getCategoryDistribution(this.startDate, this.endDate).subscribe((data) => {
      this.categories = data;
    });

    this.dashboardService.getTrend(this.startDate, this.endDate).subscribe((data) => {
      this.trend = data;
    });
  }
}
