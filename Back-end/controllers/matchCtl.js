const express = require("express");
const { model, Types } = require("mongoose");
const User = require("../models/User");
const Match = require("../models/Match");

const matchController = {
    create: async (req, res) => {
        const { round, winner, loser } = req.body;
        try {
            const winnerUser = await User.findOne({ username: winner });
            const loserUser = await User.findOne({ username: loser });
      console.log({ winnerUser });
      console.log({ loserUser });

      //   return res.json({
      //     winnerUsername,
      //     loserUsername,
      //   });
      //validate
      if (!winnerUser || !loserUser) {
        return res
          .status(400)
          .json({ success: false, message: "username not found" });
      }

      const newMatch = new Match({
        round,
        winner: new Types.ObjectId(winnerUser._id),
        loser: new Types.ObjectId(loserUser._id),
      });
      await newMatch.save();

      res.json({
        success: true,
        message: "Save match",
        match: newMatch,
      });
    } catch (err) {
      res.status(500).json(err.msg);
    }
  },

  getall: async (req, res) => {
    try {
      const matches = await Match.find({
        $or: [{ winner: req.userId }, { loser: req.userId }],
      }).populate([
        { path: "winner", strictPopulate: false },
        { path: "loser", strictPopulate: false },
      ]);
      res.status(200).json({
        success: true,
        matches,
      });
    } catch (error) {
      console.log({ error });
      res.status(500).json({
        success: false,
        message: "Internal server error",
      });
    }
  },
};

module.exports = matchController;
