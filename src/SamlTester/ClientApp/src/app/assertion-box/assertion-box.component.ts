import {Component, OnInit} from '@angular/core';
import {LaunchSetting} from "../interfaces/LaunchSetting";
import {LaunchingService} from "../services/launching.service";
import {SafePipe} from "./safe.pipe";
import {SafeHtml}  from "@angular/platform-browser";

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
  displayHtml: any
  constructor(private launchingService: LaunchingService, private safePipe: SafePipe) {
    this.submitted = false;
    this.value = '';
    this.innerHtml = undefined;
    this.displayHtml = '';
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

  renderHtml() {
    this.displayHtml = this.getInnerHTMLValue();
  }

  getInnerHTMLValue() {
    return this.launchingService.sendLaunch(this.launch).then(x => {
      let input = x as string;
      let foo = this.safePipe.transform(input, 'html');
      this.innerHtml = foo;
      return this.innerHtml;
    });
  }

  onSubmit() {
    this.trySetLaunch(this.value);
    if (this.launch === undefined || this.launch === null) {
      alert("Invalid configuration")
    }
    // TODO: Pass result of
    this.submitted = true;
    };


  ngOnInit(): void {
  }

}

