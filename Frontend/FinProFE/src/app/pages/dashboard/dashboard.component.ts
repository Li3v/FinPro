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

  formatDate(date: Date): string {
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');

    return `${year}-${month}-${day}`;
  }

  loadDashboard() {
    const params = {
      startDate: this.startDate,
      endDate: this.endDate,
    };

    if (!this.startDate || !this.endDate) {
      return;
    }

    this.dashboardService
      .getSummary(this.startDate, this.endDate)
      .subscribe((data) => {
        this.summary = data;
      });

    this.dashboardService
      .getCategoryDistribution(this.startDate, this.endDate)
      .subscribe((data) => {
        const labels = data.map((x: any) => x.categoryName);
        const values = data.map((x: any) => x.total);

        this.categories = {
          labels: labels,
          datasets: [{ data: values }],
        };
      });

    this.dashboardService
      .getTrend(this.startDate, this.endDate)
      .subscribe((data) => {
        const labels = data.map((x: any) => `${x.month}/${x.year}`);
        const income = data.map((x: any) => x.income);
        const expense = data.map((x: any) => x.expense);

        this.trend = {
          labels: labels,
          datasets: [
            {
              label: 'Income',
              data: income,
            },
            {
              label: 'Expense',
              data: expense,
            },
          ],
        };
      });
  }

  logOut() {
    this.authService.logOut();
    this.router.navigate(['/']);
  }

  ngOnInit(): void {

  const today = new Date();

  const firstDay = new Date(today.getFullYear(), today.getMonth(), 1);
  const lastDay = new Date(today.getFullYear(), today.getMonth() + 1, 0);

  this.startDate = firstDay.toLocaleDateString('en-CA');
  this.endDate = lastDay.toLocaleDateString('en-CA');

  this.currentUser = this.authService.getUserName();

  this.loadDashboard();
}
}
