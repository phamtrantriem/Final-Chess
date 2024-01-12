const express = require("express");
const { model, Types } = require("mongoose");
const Avatar = require("../models/Avatar");

const avatarController = {
    create: async (req, res) => {
        const { name } = req.body;
        try {
            const avatars = await Avatar.create({
                name: name,
                userId: new Types.ObjectId(req.userId)
            })
            res.status(200).json({
                success: true,
                avatars,
            });
        } catch (error) {
            res.status(500).json({
                success: false,
                message: "Internal server error",
            });
        }
    },
    getall: async (req, res) => {
        try {
            const avatars = await Avatar.find({
                userId: new Types.ObjectId(req.userId),
            });
            res.status(200).json({
                success: true,
                avatars,
            });
        } catch (error) {
            res.status(500).json({
                success: false,
                message: "Internal server error",
            });
        }
    },
};

module.exports = avatarController;
