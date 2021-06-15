import React from "react";
import "./AddRecipePopUp.css";
import { url } from "../../commons/properties"
import { Row, Col} from "react-bootstrap";
import { connect } from "react-redux";


export class AddRecommendationPopUp extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            recommendationId: this.props.recommendationId,
            recommendation: null,
            collaborationId: this.props.collaborationId       
        };
    }


    handleClick = () => {
        this.props.toggle();
    };


    success() {
        console.log("Dodano zalecenie!")     
    }

    error(err) {
        console.log('Nie dodano zalecenia:', err);
    }

    handleSubmit = (event) => {
        event.preventDefault();
        const collaborationId = this.props.collaborationId;
        const text = event.target.text.value;
        const currentDate = new Date();
        const timestamp = currentDate.getTime();
        const sendDate = "2021-05-16T00:00:00";

        const data = JSON.stringify({
            collaborationId: collaborationId,
            sendDate: sendDate,
            text: text
        });

        console.log(data)
        var request = new XMLHttpRequest();
        request.onload = this.success;
        request.onerror = this.error;
        request.open('POST', url + '/diet-recommendations', true);
        request.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');
        request.send(data);
        this.props.handler();
        this.setState({
            msg: "Dodano zalecenie"
        })
                    
    };

    handleEdit= (event) => {
        event.preventDefault();
        const collaborationId = this.props.collaborationId;
        const text = event.target.text.value;
        const currentDate = new Date();
        const timestamp = currentDate.getTime();
        const sendDate = "2021-05-16T00:00:00";

        const data = JSON.stringify({
            collaborationId: collaborationId,
            sendDate: sendDate,
            text: text
        });

        console.log(data)
        var request = new XMLHttpRequest();
        request.onload = this.success;
        request.onerror = this.error;
        request.open('PUT', url + '/diet-recommendations/'+this.props.recommendationId, true);
        request.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');
        request.send(data);
        this.props.handler();
        this.setState({
            msg: "Dodano zalecenie"
        })
                    
    };

    async componentDidMount() {
        
        let recommendationId = this.state.recommendationId;
        let recommendation = null;
        if (recommendationId){
           const response2 = await fetch(url + '/diet-recommendations/'+recommendationId);
        recommendation = await response2.json(); 
        } 
        this.setState({ recommendation: recommendation });

    }
    render() {
        const addedStyle = {
            color: "green",
            fontWeight: "bold"
        }
        
        //const availableProducts =  this.state.recipes2;
        let newRecommendation = false;
        let recommendation= this.state.recommendation;
        let func= this.handleEdit;
        if(recommendation === null){
            newRecommendation = true;
            func = this.handleSubmit;
            recommendation = {
                text: ""   
            };
        }

        return (
            <div /*className="modal" */>
                <div className="modal_content">
                    <span className="close" onClick={this.handleClick}>
                        &times;
          </span>
                    <form onSubmit={func}>
                        <h3>Dodaj zalecenie:</h3>
                        <Row >
                            <Col>
                                <label>                         
                                    Opis:
              <input type="text" name="text"  required defaultValue={recommendation.text} />
                                </label>                        
                            </Col>
                            
                        </Row>
                        <br />
                        {newRecommendation ? (
                            <Row>
                                <Col><input type="submit" onClick={this.handleSend} value="Dodaj" /></Col>  
                                <Col><div style = {addedStyle}>{this.state.msg}</div></Col>
                            </Row>
                        ) : (
                            <Row>
                            <Col><input type="submit" onClick={this.handleSend} value="Edytuj" /></Col>  
                            <Col><div style = {addedStyle}>{this.state.msg}</div></Col>
                        </Row> 
                        )}
                    </form>
                </div>
            </div>
        );
    }
}

const mapStateToProps = (state) => {
    return {    

    };
};

AddRecommendationPopUp = connect(mapStateToProps)(AddRecommendationPopUp);