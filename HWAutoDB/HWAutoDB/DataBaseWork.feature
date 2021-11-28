Feature: DataBaseWork
	As a user
	I want the database to correctly process queries

@InsertData
Scenario Outline: It is possible to insert data to XSHOPX DB
	When I create row in table 'Persons' with data
		| FirstName   | LastName   | Age   | City   |
		| <firstName> | <lastName> | <age> | <city> |
	When I select whole 'Persons' table
	Then Last row in table Persons contains data
		| FirstName   | LastName   | Age   | City   |
		| <firstName> | <lastName> | <age> | <city> |
	When I delete last row in table 'Persons'

	Examples:
		| firstName   | lastName | age | city        |
		| Worthington | McGurn   | 36  | Santa Clara |

@InvalidData
Scenario Outline: Insert invalid data to XSHOPX DB
	When I try to create row in table 'Persons' with data longer then 20 chars
		| FirstName   | LastName   | Age   | City   |
		| <firstName> | <lastName> | <age> | <city> |
	Then I get an error message '2628' in response

	Examples:
		| firstName             | lastName              | age | city                  |
		| LongerThanTwentyChars | Nevredim              | 19  | Lviv                  |
		| Vadim                 | LongerThanTwentyChars | 34  | Odessa                |
		| Galka                 | Sraka                 | 59  | LongerThanTwentyChars |