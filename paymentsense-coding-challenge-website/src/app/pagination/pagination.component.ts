import { Component, EventEmitter, Input, OnChanges, OnDestroy, OnInit, Output } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss']
})
export class PaginationComponent implements OnChanges {
  @Input() totalPages: number;
  @Output() goTo: EventEmitter<number> = new EventEmitter<number>();

  public currentPage = 1;
  public pages: number[];

  constructor() { }

  ngOnChanges(): void {
    this.pages = [];
    for (let i = 1; i <= this.totalPages; i++) {
      this.pages.push(i);
    }
  }

  public onGoTo(page: number): void {
    this.currentPage = page;
    this.goTo.emit(page);
  }

  public onNext(): void {
    this.currentPage += 1;
    this.goTo.emit(this.currentPage);
  }

  public onPrevious(): void {
    this.currentPage -= 1;
    this.goTo.emit(this.currentPage);
  }

}
