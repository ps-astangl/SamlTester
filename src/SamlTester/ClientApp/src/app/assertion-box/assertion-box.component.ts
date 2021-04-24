import {Component, OnInit} from '@angular/core';
import {LaunchSetting} from "../interfaces/LaunchSetting";
import {LaunchingService} from "../services/launching.service";
import {SafePipe} from "./safe.pipe";

@Component({
  selector: 'app-assertion-box',
  templateUrl: './assertion-box.component.html',
  styleUrls: ['./assertion-box.component.css']
})

export class AssertionBoxComponent implements OnInit {
  submitted: boolean
  value: string
  launchConfiguration: any
  launchHtml: string | void | undefined

  constructor(private launchingService: LaunchingService, private safePipe: SafePipe) {
    this.submitted = false;
    this.value = '';
  }

  trySetLaunch(value: string): LaunchSetting {
    try {
      let parsed: LaunchSetting = JSON.parse(value);
      this.launchConfiguration = parsed;
      return this.launchConfiguration;
    } catch (e) {
      console.error(e)
      return this.launchConfiguration;
    }
  }

  onSubmit() {
    this.trySetLaunch(this.value);
    if (this.launchConfiguration === undefined || this.launchConfiguration === null) {
      alert("Invalid configuration")
    }

    this.launchingService.sendLaunch(this.launchConfiguration).then(result => {
      console.log(result);
      this.launchHtml = result;
    });
    this.submitted = true;
  };

  ngOnInit(): void {
  }

}

