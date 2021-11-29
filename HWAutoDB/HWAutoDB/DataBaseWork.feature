Feature: DataBaseWork
	As a user
	I want the database to correctly process queries

@InsertData
Scenario: It is possible to insert data in XSHOPX table Persons
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

@UpdateData
Scenario: It is possible to update data in XSHOPX table Persons
	When I create row in table 'Persons' with data
		| FirstName   | LastName   | Age   | City   |
		| <firstName> | <lastName> | <age> | <city> |
	When I update last row in table 'Persons' with new data
		| FirstName      | LastName      | Age      | City      |
		| <firstNameNew> | <lastNameNew> | <ageNew> | <cityNew> |
	When I select whole 'Persons' table
	Then Last row in table Persons contains data
		| FirstName      | LastName      | Age      | City      |
		| <firstNameNew> | <lastNameNew> | <ageNew> | <cityNew> |
	When I delete last row in table 'Persons'

	Examples:
		| firstName   | lastName | age | city        | firstNameNew | lastNameNew | ageNew | cityNew |
		| Worthington | McGurn   | 36  | Santa Clara | Vadim        | Nevredim    | 19     | Lviv    |

@InvalidData
Scenario Outline: It is impossible to insert invalid data longer then 20 chars in XSHOPX table Persons
	When I try to create row in table Persons 'Persons'
		| FirstName   | LastName   | Age   | City   |
		| <firstName> | <lastName> | <age> | <city> |
	Then I get an error message '2628' in response

	Examples:
		| firstName             | lastName              | age | city                  |
		| LongerThanTwentyChars | Nevredim              | 19  | Lviv                  |
		| Vadim                 | LongerThanTwentyChars | 34  | Odessa                |
		| Galka                 | Sraka                 | 59  | LongerThanTwentyChars |

@InvalidData
Scenario: It is impossible to insert invalid age in string format in XSHOPX table Persons
	When I try to create row in table Persons 'Persons'
		| FirstName | LastName | Age     | City   |
		| Galka     | Sraka    | ImYoung | Odessa |
	Then I get an error message '245' in response

@InvalidData
Scenario: It is impossible to insert invalid orderPrice in string format in XSHOPX table Orders
	When I try to create row in table Orders 'Orders'
		| OrderId | Product | OrderPrice |
		| 29      | Avocado | StoUAH     |
	Then I get an error message '544' in response

@EmptyData
Scenario: It is impossible to insert data without not filling in the required field Product in table Orders
	When I create row in table 'Orders' without Product field
		| OrderId | OrderPrice |
		| 29      | 100        |
	Then I get an error message '544' in response

@EmptyData
Scenario: It is impossible to insert data without not filling in the required field FirstName in table Persons
	When I create row in table 'Persons' without FirstName field
		| LastName | Age | City   |
		| Nevredim | 34  | Odessa |
	Then I get an error message '515' in response

@EmptyData
Scenario: It is impossible to insert data without not filling in the required field LastName in table Persons
	When I create row in table 'Persons' without LastName field
		| FirstName | Age | City   |
		| Vadim     | 34  | Odessa |
	Then I get an error message '515' in response

@EmptyData
Scenario: It is impossible to insert data without not filling in the required field Age in table Persons
	When I create row in table 'Persons' without Age field
		| FirstName | LastName | City   |
		| Vadim     | Nevredim | Odessa |
	Then I get an error message '515' in response

@EmptyData
Scenario: It is impossible to insert data without not filling in the required field City in table Persons
	When I create row in table 'Persons' without City field
		| FirstName | LastName | Age |
		| Vadim     | Nevredim | 34  |
	Then I get an error message '515' in response