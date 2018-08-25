Feature: Find A Course
	As a user
	I am able to access and use the Find a Course Page
	
@regression
	Scenario Outline: User search on Find a Course page
		Given I navigate to Find a Course home page
		When I enter course <CourseName>
		And I select qualification <QualificationLevel>
		And I enter location <Location> 
		And I select distance <Distance>
		And I click Search
#		Then I should be on Search Results for page

  Examples:
    | CourseName  | QualificationLevel                                   | Location   | Distance |
    | Chemistry   | Entry level - Skills for Life                        | Birmingham | 1 Mile   |
    | Bricklaying | Level 1 - First certificate                          | London     | 3 Miles  |
    | Maths       | Level 2 - GCSE/O level                               | London     | 5 Miles  |
    | English     | Level 3 - A level/Access to higher education diploma | Birmingham | 10 Miles |
    | Plumbing    | Level 4 - Certificate of higher education/HNC        | London     | 15 Miles |
    | Electronic  | Level 5 - Foundation degree/HND                      | London     | 20 Miles |
    | Medicine    | Level 6 - Degree/Graduate diploma                    | Birmingham | National |
    | Biology     | Level 7 - Masters Degree/Postgraduate diploma        | London     | 3 Miles  |
    | Physics     | Level 8 - Doctorate/PhD                              | London     | 5 Miles  |


@regression
	Scenario: User contacts adviser from Find a Course page
		Given I navigate to Find a Course home page
		When I click Contact an adviser link
		Then I will be on Contact us page


@BrowserStack
Scenario Outline: BrowserStack Test Find a Course
  Given I am on the google page for <profile> and <environment>
		When Using BrowserStack I enter course <CourseName>
		And Using BrowserStack I select qualification <QualificationLevel>
		And Using BrowserStack I enter location <Location> 
		And Using BrowserStack I select distance <Distance>
		And Using BrowserStack I click Search
	#	Then I should be on Search Results for page

  Examples:
        | profile  | environment | CourseName  | QualificationLevel            | Location   | Distance |
        | single   | chrome      | Chemistry   | Entry level - Skills for Life | Birmingham | 1 Mile   |
    #   | parallel | safari      | Bricklaying | Level 1 - First certificate   | London     | 3 Miles  |
        | parallel | chrome      | Bricklaying | Level 1 - First certificate   | London     | 3 Miles  |
        | parallel | firefox     | Bricklaying | Level 1 - First certificate   | London     | 3 Miles  |
        | parallel | ie          | Bricklaying | Level 1 - First certificate   | London     | 3 Miles  |

