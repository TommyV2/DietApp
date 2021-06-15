import { createStore, applyMiddleware } from "redux";
import thunk from "redux-thunk";
import {myreducer} from "./myreducer";


const mystore = createStore(
    myreducer, 
    {userData: [], userCollaborations: [], userInvitations: [], userList: [], availableNutrients: [], recommendations: [], dietPlans: [], availableRecipes: []},
    applyMiddleware(thunk)
  );
  export default mystore;

