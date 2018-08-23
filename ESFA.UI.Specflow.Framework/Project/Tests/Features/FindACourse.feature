Feature: Find A Course
	As a user
	I am able to search the Find a Course Page
	@regression
	Scenario Outline: User search on Find a Course page
		Given I navigate to Find a Course home page
		When I enter course <CourseName>
		And I select qualification <QualificationLevel>
		And I enter location <Location> 
		And I select distance <Distance>
		And I click Search
		Then I should be on Find a Course Search Results page

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