import React from "react";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import { MainPage } from "./pages/MainPage";
import { NoMatch } from "./pages/NoMatch"
import { SpecialistDashboard } from "./pages/specialist/SpecialistDashboard";
import { SpecialistProfile } from "./pages/specialist/SpecialistProfile";
import { ClientProfile } from "./pages/client/ClientProfile";
import { ClientDietPage } from "./pages/client/ClientDietPage";
import { ClientTrainingPage } from "./pages/client/ClientTrainingPage";
import { ClientDashboard } from "./pages/client/ClientDashboard";
import { ClientCollaboration } from "./pages/client/ClientCollaboration";
import { SpecialistCollaboration } from "./pages/specialist/SpecialistCollaboration";
import {ClientMainPage} from "./pages/client/ClientMainPage";
import {SpecialistMainPage} from "./pages/specialist/SpecialistMainPage";
import { SpecialistMealsPage } from "./pages/specialist/SpecialistMealsPage";
import { SpecialistExcersisesPage } from "./pages/specialist/SpecialistExcersisesPage";
import { SpecialistDietPlansPage } from "./pages/specialist/SpecialistDietPlansPage";
import { SpecialistTrainingPlansPage } from "./pages/specialist/SpecialistTrainingPlansPage.js";
import {SpecialistDietPlanPage} from "./pages/specialist/SpecialistDietPlanPage.js";
import {ClientDietPlanPage} from "./pages/client/ClientDietPlanPage.js";

class App extends React.Component {
  render() {
    return (
      <>
        <Router>
          <Switch>
            <Route exact path="/" component={MainPage} />
            <Route exact path="/client" component={ClientMainPage} />
            <Route path="/client/dashboard" component={ClientDashboard} />
            <Route path="/client/profile" component={ClientProfile} />
            <Route path="/client/diet" component={ClientDietPage} />
            <Route path="/client/training" component={ClientTrainingPage} />
            <Route exact path="/client/collaboration/:id" render={(props) => <ClientCollaboration {...props} />}/>
            <Route exact path="/client/collaboration/:collaborationId/diet_plan/:dietPlanId" render={(props) => <ClientDietPlanPage {...props} />} />

            <Route exact path="/specialist" component={SpecialistMainPage} />
            <Route path="/specialist/dashboard" component={SpecialistDashboard} />
            <Route path="/specialist/profile" component={SpecialistProfile} />
            <Route exact path="/specialist/collaboration/:id" render={(props) => <SpecialistCollaboration {...props} />}/>
            <Route path="/specialist/meals" component={SpecialistMealsPage} />
            <Route path="/specialist/excersises" component={SpecialistExcersisesPage} />
            <Route path="/specialist/diet_plans" component={SpecialistDietPlansPage} />
            <Route path="/specialist/training_plans" component={SpecialistTrainingPlansPage} />
            <Route exact path="/specialist/collaboration/:collaborationId/diet_plan/:dietPlanId" render={(props) => <SpecialistDietPlanPage {...props} />} />


            <Route component={NoMatch} />
          </Switch> 
        </Router>
        
      </>
    );
  }
}

export default App;