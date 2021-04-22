import {Component, OnInit} from '@angular/core';


interface Issuer {
  value: string;
  viewValue: string;
}

@Component({
  selector: 'app-assertion-box',
  templateUrl: './assertion-box.component.html',
  styleUrls: ['./assertion-box.component.css']
})

export class AssertionBoxComponent implements OnInit {
  submitted: boolean
  value: string;
  issuerName: string;
  issuerList: Issuer[];

  constructor() {
    this.submitted = false;
    this.value = '';
    this.issuerName = '';
    this.issuerList = [
      {viewValue: 'Test API', value: 'https://testapi.crisphealth.org/Screening/metadata/crisphealth.org'},
      {viewValue: 'Localhost', value: 'https://localhost:5001/'}
    ]
  }


  onSubmit() {
    this.submitted = true;
    alert(this.value)
    alert(this.issuerName)
  }

  ngOnInit(): void {
  }

}
