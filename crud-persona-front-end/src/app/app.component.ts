import { ChangeDetectorRef, Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { LoandingService } from './services/loanding.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  isLoanding: boolean = false;

  constructor(private loandingService: LoandingService,
    private cdref: ChangeDetectorRef){
    this.getLoanding();
  }

  ngAfterContentChecked(): void {
    this.cdref.detectChanges();
  }

  getLoanding(){
    this.loandingService.getLoanding().subscribe(l => {
      this.isLoanding = l;
    })
  }

}