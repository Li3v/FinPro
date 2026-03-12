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
  categories: any;
  currentUser: any;
  endDate: string = '';
  startDate: string = '';
  summary: any;
  trend: any;

  constructor(
    private authService: AuthService,
    private router: Router,
    private dashboardService: DashboardService,
  ) {}

  loadDashboard() {
    const params = {
      startDate: this.startDate,
      endDate: this.endDate,
    };

    this.dashboardService
      .getSummary(this.startDate, this.endDate)
      .subscribe((data) => {
        this.summary = data;
      });

    this.dashboardService
      .getCategoryDistribution(this.startDate, this.endDate)
      .subscribe((data) => {
        this.categories = data;
      });

    this.dashboardService
      .getTrend(this.startDate, this.endDate)
      .subscribe((data) => {
        this.trend = data;
      });
  }

  logOut() {
    this.authService.logOut();
    this.router.navigate(['/']);
  }

  ngOnInit(): void {
   // this.loadDashboard();
    this.currentUser = this.authService.getUserName();
  }
}
