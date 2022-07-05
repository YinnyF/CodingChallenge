import { ChangeDetectionStrategy, Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PaginationComponent implements OnInit, OnDestroy {
  public totalPages$ = new Subject<number>();
  private destroy$ = new Subject();

  @Input() set totalPages(number) {
    console.log("input given");
    this.totalPages$.next(number);
  }
  @Output() goTo: EventEmitter<number> = new EventEmitter<number>();

  public currentPage = 1;
  public pages: number[];

  constructor() { }

  ngOnInit(): void {
    this.totalPages$.pipe(
      takeUntil(this.destroy$)
    )
    .subscribe({
      next: (noOfPages) => {
        this.pages = [];
        for (let i = 1; i <= noOfPages; i++) {
          this.pages.push(i);
        }
      }
    })
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
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
