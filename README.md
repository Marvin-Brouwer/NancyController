# Nancy Controller
Using Nancy the way you're used to in Mvc.Net

## About
This library provides a NancControllerModule, this creates a Nancy Module which binds methods to routes  
similar to the way it looks and feels in Mvc.Net.

## Installation
Install ge nuget Package: [NancyController](https://www.nuget.org/packages/NancyController/)
`PM > Install-Package NancyController`

## Usage
The usage is very similar to the normal way, if you'd like to see an example you can check it out here: [/Source/NancyController.Poc](https://github.com/Marvin-Brouwer/CodedViews/tree/master/Source/NancyController.Poc).

	public sealed class HomeModule : NancyControllerModule
    {
        public Negotiator Index()
        {
            return View["Index"];
        }
	}

