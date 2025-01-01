Feature: Counter
![Calculator](https://specflow.org/wp-content/uploads/2020/09/calculator.png)
    
Rule: The counter must increase by on on every button click

Scenario: Increase coutner
    Given the counter page is visible and counter is 0
    When button is clicked
    Then counter is 1