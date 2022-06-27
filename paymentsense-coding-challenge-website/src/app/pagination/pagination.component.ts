import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss']
})
export class PaginationComponent implements OnChanges {
  @Input() currentPage = 0;
  @Input() totalPages = 0;

  @Output() goTo: EventEmitter<number> = new EventEmitter<number>();
  @Output() next: EventEmitter<number> = new EventEmitter<number>();
  @Output() previous: EventEmitter<number> = new EventEmitter<number>();

  public pages: number[] = [];

  constructor() { }

  ngOnChanges(changes: SimpleChanges): void {
    if (
      (changes.currentPage && changes.currentPage.currentValue) ||
      (changes.totalPages && changes.totalPages.currentValue)
    ) {
      this.pages = this.getPages(this.currentPage, this.totalPages);
    }
  }

  public onGoTo(page: number): void {
    this.goTo.emit(page);
  }

  public onNext(): void {
    this.next.emit(this.currentPage);
  }

  public onPrevious(): void {
    this.previous.emit(this.currentPage);
  }

  private getPages(currentPage: number, totalPages: number): number[] {
    const pages = [];
    for (let i = 1; i <= totalPages; i++) {
      pages.push(i);
    }
    return pages;
  }

}
