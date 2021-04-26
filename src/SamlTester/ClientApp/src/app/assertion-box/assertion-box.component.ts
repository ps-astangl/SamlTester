import {Component, OnInit} from '@angular/core';
import {LaunchingService} from "../services/launching.service";
import {SamlAttribute, SamlLaunchConfiguration} from "../interfaces/LaunchSetting";

@Component({
  selector: 'app-assertion-box',
  templateUrl: './assertion-box.component.html',
  styleUrls: ['./assertion-box.component.css']
})

export class AssertionBoxComponent implements OnInit {
  submitted: boolean
  value: string
  launchConfiguration: SamlLaunchConfiguration
  launchHtml: string | void | undefined

  constructor(private launchingService: LaunchingService) {
    this.submitted = false;
    this.value = '';
    this.launchConfiguration = new SamlLaunchConfiguration([], '');
  };

  trySetLaunch(value: string): SamlLaunchConfiguration {
    try {
      this.launchConfiguration = JSON.parse(value);
      return this.launchConfiguration;
    } catch (e) {
      console.error(e)
      return this.launchConfiguration;
    }
  }

  setDefaultSettings() {
    let issuer = "Screening"
    let samlAttributes = [
      new SamlAttribute("User", "Test User"),
      new SamlAttribute("Organization", "SJMC"),
      new SamlAttribute("PatientEid", "31131416")
    ]

    console.log("Setting defaults...")
    this.launchConfiguration = new SamlLaunchConfiguration(samlAttributes, issuer);
    console.log(this.launchConfiguration)
    return this.launchConfiguration;
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
    this.value = JSON.stringify(this.setDefaultSettings(), null, ' ');
  }

  //TODO: Handle GET /Configurations
  //TODO: Handle PUT /Configurations
  //TODO: Handle POST /Configurations
}

