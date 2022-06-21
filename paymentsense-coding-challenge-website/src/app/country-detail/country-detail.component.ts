import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Country } from '../Country';

@Component({
  selector: 'app-country-detail',
  templateUrl: './country-detail.component.html',
  styleUrls: ['./country-detail.component.scss']
})
export class CountryDetailComponent implements OnInit {

  @Input() country?: Country;

  @Output() onClose = new EventEmitter<boolean>();
  
  constructor() { }

  ngOnInit() {
  }

  closeCountryDetail() {
    // console.log("close country detail clicked!");
    this.onClose.emit();
  }
}
