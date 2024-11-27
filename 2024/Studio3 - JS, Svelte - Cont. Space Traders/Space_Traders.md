## Space traders Code Samples and Explanations link to website 
Link : https://spacetraders.stoplight.io/docs/spacetraders/93c5d5e6ad5b0-list-factions
 
 ##  Registering a New Agent / Character 
   
  The user Will make a unique name for each of their agents e.g. ( "ZERO_SHOT", " SP4CE_TR4DER") and then pick a faction that will pick where they start.
  Then the User will be given a token which they will be given at the start which can be accessed in their profile 

## Code Creating Character
  Register Code call
  ```
    https://api.spacetraders.io/v2/register
    
  ```
Request Sample: Shell / cURL
   ```
   curl --request POST \
 --url 'https://api.spacetraders.io/v2/register' \
 --header 'Content-Type: application/json' \
 --data '{
    "symbol": "INSERT_CALLSIGN_HERE",
    "faction": "COSMIC",
    "Email": "string"
   }'
   ```

Response Example
```
{
  "data": {
    "agent": {
      "accountId": "string",
      "symbol": "string",
      "headquarters": "string",
      "credits": 0,
      "startingFaction": "string",
      "shipCount": 0
    },
    "contract": {
      "id": "string",
      "factionSymbol": "string",
      "type": "PROCUREMENT",
      "terms": {
        "deadline": "2019-08-24T14:15:22Z",
        "payment": {
          "onAccepted": 0,
          "onFulfilled": 0
        },
        "deliver": [
          {
            "tradeSymbol": "string",
            "destinationSymbol": "string",
            "unitsRequired": 0,
            "unitsFulfilled": 0
          }
        ]
      },
      "accepted": false,
      "fulfilled": false,
      "expiration": "2019-08-24T14:15:22Z",
      "deadlineToAccept": "2019-08-24T14:15:22Z"
    },
    "faction": {
      "symbol": "COSMIC",
      "name": "string",
      "description": "string",
      "headquarters": "string",
      "traits": [
        {
          "symbol": "BUREAUCRATIC",
          "name": "string",
          "description": "string"
        }
      ],
      "isRecruiting": true
    },
    "ship": {
      "symbol": "string",
      "registration": {
        "name": "string",
        "factionSymbol": "string",
        "role": "FABRICATOR"
      },
      "nav": {
        "systemSymbol": "string",
        "waypointSymbol": "string",
        "route": {
          "destination": {
            "symbol": "string",
            "type": "PLANET",
            "systemSymbol": "string",
            "x": 0,
            "y": 0
          },
          "departure": {
            "symbol": "string",
            "type": "PLANET",
            "systemSymbol": "string",
            "x": 0,
            "y": 0
          },
          "origin": {
            "symbol": "string",
            "type": "PLANET",
            "systemSymbol": "string",
            "x": 0,
            "y": 0
          },
          "departureTime": "2019-08-24T14:15:22Z",
          "arrival": "2019-08-24T14:15:22Z"
        },
        "status": "IN_TRANSIT",
        "flightMode": "CRUISE"
      },
      "crew": {
        "current": 0,
        "required": 0,
        "capacity": 0,
        "rotation": "STRICT",
        "morale": 0,
        "wages": 0
      },
      "frame": {
        "symbol": "FRAME_PROBE",
        "name": "string",
        "description": "string",
        "condition": 0,
        "moduleSlots": 0,
        "mountingPoints": 0,
        "fuelCapacity": 0,
        "requirements": {
          "power": 0,
          "crew": 0,
          "slots": 0
        }
      },
      "reactor": {
        "symbol": "REACTOR_SOLAR_I",
        "name": "string",
        "description": "string",
        "condition": 0,
        "powerOutput": 1,
        "requirements": {
          "power": 0,
          "crew": 0,
          "slots": 0
        }
      },
      "engine": {
        "symbol": "ENGINE_IMPULSE_DRIVE_I",
        "name": "string",
        "description": "string",
        "condition": 0,
        "speed": 1,
        "requirements": {
          "power": 0,
          "crew": 0,
          "slots": 0
        }
      },
      "cooldown": {
        "shipSymbol": "string",
        "totalSeconds": 0,
        "remainingSeconds": 0,
        "expiration": "2019-08-24T14:15:22Z"
      },
      "modules": [
        {
          "symbol": "MODULE_MINERAL_PROCESSOR_I",
          "capacity": 0,
          "range": 0,
          "name": "string",
          "description": "string",
          "requirements": {
            "power": 0,
            "crew": 0,
            "slots": 0
          }
        }
      ],
      "mounts": [
        {
          "symbol": "MOUNT_GAS_SIPHON_I",
          "name": "string",
          "description": "string",
          "strength": 0,
          "deposits": [
            "QUARTZ_SAND"
          ],
          "requirements": {
            "power": 0,
            "crew": 0,
            "slots": 0
          }
        }
      ],
      "cargo": {
        "capacity": 0,
        "units": 0,
        "inventory": [
          {
            "symbol": "string",
            "name": "string",
            "description": "string",
            "units": 1
          }
        ]
      },
      "fuel": {
        "current": 0,
        "capacity": 0,
        "consumed": {
          "amount": 0,
          "timestamp": "2019-08-24T14:15:22Z"
        }
      }
    },
    "token": "string"
  }
}
```





## Factions  

## All available Faction Symbols that could be used:
- CORSAIRS
- UNITED
- CULT

##  Code for Creating Factions
   Symbols is a string >= 1 characters
   Name is a String >= 1 characters
   The description is a String >= 1 characters
   Headquarters is a String >= 1 characters
   Traits is an array[object]
   Is recruiting is a boolean
   
   Request Sample
  ```
curl --request GET \
  --url https://api.spacetraders.io/v2/factions \
  --header 'Accept: application/json' \
  --header 'Authorization: Bearer undefined'
  ```

  Response Example 
   ```
  "data": [
    {
      "symbol": "COSMIC",
      "name": "string",
      "description": "string",
      "headquarters": "string",
      "traits": [
        {
          "symbol": "BUREAUCRATIC",
          "name": "string",
          "description": "string"
        }
      ],
      "isRecruiting": true
    }
  ],
```

   

   
