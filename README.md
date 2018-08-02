# copay.asia
Test project for copay.asia

project uses .NET Core

just install current(2.1.302) sdk, build and lauch project.

all documentation available at https://localhost:5001/index.html

also you need ganache-cli as test network.

# examples of output

Show all addresses in wallet. 

Request: 

GET https://localhost:5001/api/ethereum/addresses

Response:

![all addresses in wallet](https://github.com/VProgNet/copay.asia/raw/master/docs/screenshots/all_addresses.png)


Show balance at address in eth

Request:

GET https://localhost:5001/api/ethereum/balance/0x4b54d6c84fff6958aa194c0d8954ef308a840cc3

Response:

![balance](https://github.com/VProgNet/copay.asia/raw/master/docs/screenshots/balance.png)


Example of sendTransaction

Request:

POST https://localhost:5001/api/ethereum/transaction/send

Request and response in one screenshot

![post transaction](https://github.com/VProgNet/copay.asia/raw/master/docs/screenshots/send_transaction.png)


Show all transactions related to address

Request:

GET https://localhost:5001/api/ethereum/history/0x09714c59a987995a8d6fde74a4ca76f5ddee4857

Response:

![all transactions](https://github.com/VProgNet/copay.asia/raw/master/docs/screenshots/transaction_history.png)

Show transaction state

Request: 

GET https://localhost:5001/api/ethereum/transaction/state/0xd47e2c5f9623721aeafeba8160c3c028bc7bc5cd300bfaebb9feedf1d95add31

Response:

![transaction state](https://github.com/VProgNet/copay.asia/raw/master/docs/screenshots/transaction_state.png)

