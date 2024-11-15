# Fast Food Delivery

1. What does the user do?
    - receive orders from customers
    - orders the correct food and gets a driver to pick it up
    - driver fills digital form when delivered

2. What data is involved?
    - orderer (name, room, hotel name/location)
    - order contents (food drink etc)
    - restaurant the food is cooked in
    - delivery driver

3. What is the main objective?
    - produce stats of orders

4. What data is needed?
    - Order ID, auto-gen
    - Date and time made
    - date and time delivered

5. What are the input use cases?
    - customer entering order
    - driver filling digital time-sheet

6. What is the first data model?
    | Order |
    | --- |
    | ID |
    | Placed date |
    | Placed time |
    | Delivered date |
    | Delivered time |

7. What are the output use cases?
    - Report weekly of stats generated


# A budget system for personal/home use

1. What does the user do?
    - Knows generally how much money they get each week
    - Checks each time they need to spend money if they have enough
    - Spend money on items

2. What data is involved?
    - Get house incomes: Money in
    - Checks bank acct: current money
    - Spend money: money out, store name/ID, spend type

3. What is the main objective of the system?
    - Record each transaction
    - Show current money status, how much avg money is left for each spending type
    - Give weekly report

4. What data is needed to satisfy this objective?
    - Record each transaction:
        - (cost/income, store name, shop type, date)
    - Show current money status, how much avg money is left for each spending type:
        - (cost/income, store name, shop type, date)
    - Give weekly report:
        - (cost/income, store name, shop type, date)

5. What are the input use cases?
    - Users enter spend type after each spend as some stores can be multiple types

6. What is the first data model?

    | Transaction |
    | --- |
    | Amount |
    | Store Name/ID |
    | Type |
    | Date |

7. What are the output use cases?
    - Show current money status
    - weekly report
