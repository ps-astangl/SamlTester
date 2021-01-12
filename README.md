# Saml Testing tool

## Configuration
The appsettings.json contains configuration values for the SAML library. The configurations need to updated with the intended pfx certificate and corresponding password in both the `LocalCertificates` and `PartnerCertificates` blocks. Both are required for a successful launch, though only the `LocalCertificates` is actually used for signing the SAML, so feel free to set `PartnerCertificates` to the same value as `LocalCertificates`.



## Endpoints
The tool exposes the following endpoint for initiating a SAML launch. 

```
POST https://localhost:5002/api

Headers: { "content-type": "applicaiton/json" }

Body:
{
    "issuer": "https://athenanet.athenahealth.com",
    "targetEndpoint": "https://localhost:5001/authorization/test",
    "attributes": [
        {
            "name": "subject",
            "value": "ETEST"
        },
        {
            "name": "NPInumber",
            "value": "1234567890"
        },
        {
            "name": "patientid",
            "value": "E2651"
        },
        {
            "name": "practiceid",
            "value": "1234"
        }
    ]
}

Returns: A redirection page containing a form with the base 64 encoded SAML response and a trigger to submit the form to the target endpoint
```


## Running the tool
A postman collection can be downloaded [here](https://www.getpostman.com/collections/552e7363165d221a2baa). If you want to make your own collection, make sure to follow the suggestion outlined [here](https://stackoverflow.com/questions/58989684/postman-render-html-response-and-execute-javascript) in order to enable Javascript in postman and allow the form to submit.