import {Component, OnInit} from '@angular/core';
import { LaunchSetting } from "../interfaces/LaunchSetting";
import {LaunchingService} from "../services/launching.service";
import {DomSanitizer} from "@angular/platform-browser";


@Component({
  selector: 'app-assertion-box',
  templateUrl: './assertion-box.component.html',
  styleUrls: ['./assertion-box.component.css']
})

export class AssertionBoxComponent implements OnInit {
  submitted: boolean
  value: string
  launch: any
  innerHtml: string|void|any
  constructor(private launchingService: LaunchingService, private sanitizer: DomSanitizer) {
    this.submitted = false;
    this.value = '';
    this.innerHtml = undefined;
  }



  trySetLaunch(value : string): LaunchSetting {
    try {
      let parsed:LaunchSetting = JSON.parse(value);
      this.launch = parsed;
      return this.launch;
    }
    catch (e) {
      // NOOP
      return this.launch;
    }
  }

  onSubmit() {
    this.trySetLaunch(this.value);
    if (this.launch === undefined || this.launch === null) {
      alert("Invalid configuration")
    }
    this.submitted = true;

    console.log("Sending launch")
    this.launchingService.sendLaunch(this.launch).then(x => {
      this.innerHtml = x;
      // this.innerHtml = this.sanitizer.sanitize(this.innerHtml)
      console.log(this.innerHtml);
    });
  }

  ngOnInit(): void {
  }

}
