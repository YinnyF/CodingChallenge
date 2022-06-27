import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss']
})
export class PaginationComponent implements OnInit, OnDestroy {
  @Input() totalPages$: Observable<number>;
  @Output() goTo: EventEmitter<number> = new EventEmitter<number>();

  public totalPages = 0;
  public currentPage = 1;
  public pages: number[] = [];

  private destroy$: Subject<void> = new Subject<void>();

  constructor() { }

  ngOnInit(): void {
    this.totalPages$.pipe(
      takeUntil(this.destroy$)
    )
      .subscribe(pages => {
        for (let i = 1; i <= pages; i++) {
          this.pages.push(i);
        }
      });
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

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.unsubscribe();
  }
}
